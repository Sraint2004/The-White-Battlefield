Shader "Unlit/snowShader"
{
    Properties{
        _MainTex("Main Tex", 2D) = "white"{} // 纹理贴图
        _Color("Color", Color) = (1,1,1,1)   // 控制纹理贴图的颜色

        _NormalMap("Normal Map", 2D) = "bump"{} // 表示当该位置没有指定任何法线贴图时，就使用模型顶点自带的法线
        _BumpScale("Bump Scale", Float) = 1  // 法线贴图的凹凸参数。为0表示使用模型原来的发现，为1表示使用法线贴图中的值。大于1则凹凸程度更大。

        _Snow ("Snow Level", Range(0,1)) = 0
        _SnowColor ("Snow Color", Color) = (1.0,1.0,1.0,1.0)
        _SnowDirection ("Snow Direction", Vector) = (0,1,0)

    }
    SubShader{
        Pass {
            // 只有定义了正确的LightMode才能得到一些Unity的内置光照变量
            Tags { "RenderType"="Opaque" "LightMode"="ForwardBase"}
            LOD 200

            CGPROGRAM

            // 包含unity的内置的文件，才可以使用Unity内置的一些变量
            #include "Lighting.cginc" // 取得第一个直射光的颜色_LightColor0 第一个直射光的位置_WorldSpaceLightPos0（即方向）
            #pragma vertex vert
            #pragma fragment frag
            
            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST; // 命名是固定的贴图名+后缀"_ST"，4个值前两个xy表示缩放，后两个zw表示偏移
            sampler2D _NormalMap;
            float4 _NormalMap_ST; // 命名是固定的贴图名+后缀"_ST"，4个值前两个xy表示缩放，后两个zw表示偏移
            float _BumpScale;    


            float _Snow;
            float4 _SnowColor;
            float4 _SnowDirection;

            struct a2v
            {
                float4 vertex : POSITION;    // 告诉Unity把模型空间下的顶点坐标填充给vertex属性
                float3 normal : NORMAL;        // 不再使用模型自带的法线。保留该变量是因为切线空间是通过（模型里的）法线和（模型里的）切线确定的。
                float4 tangent : TANGENT;    // tangent.w用来确定切线空间中坐标轴的方向的。
                float4 texcoord : TEXCOORD0; 
            };

            struct v2f
            {
                float4 uv : TEXCOORD0; // xy存储MainTex的纹理坐标，zw存储NormalMap的纹理坐标
                float4 vertex : SV_POSITION; // 声明用来存储顶点在裁剪空间下的坐标
                float3 normal : NORMAL;  
                float3 lightDir : TEXCOORD1;   // 切线空间下，平行光的方向
                float3 tangentToWorld1:TEXCOORD3;          //切线坐标转为世界坐标
                float3 tangentToWorld2:TEXCOORD4;
                float3 tangentToWorld3:TEXCOORD5;

            };

            // 计算顶点坐标从模型坐标系转换到裁剪面坐标系
            v2f vert(a2v v)
            {
                v2f o;
                //顶点坐标转换
                // 该步骤用来把一个坐标从模型空间转换到剪裁空间
                o.vertex = UnityObjectToClipPos(v.vertex); 

                //获取法线（把法线方向从模型空间转换到世界空间）。
                o.normal = UnityObjectToWorldNormal(v.normal);

                //把法线的从切线空间转到世界空间作准备
                float3 worldNormal = o.normal;
                float3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
                float3 worldBinormal = cross(worldNormal,worldTangent)*v.tangent.w;
                o.tangentToWorld1 = float3(worldTangent.x,worldBinormal.x,worldNormal.x);
                o.tangentToWorld2 = float3(worldTangent.y,worldBinormal.y,worldNormal.y);
                o.tangentToWorld3 = float3(worldTangent.z,worldBinormal.z,worldNormal.z);


                //贴图的纹理坐标
                o.uv.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw; 
                //法线贴图的纹理坐标
                o.uv.zw = v.texcoord.xy * _NormalMap_ST.xy + _NormalMap_ST.zw; 

                //调用这个宏会得到一个矩阵rotation，该矩阵用来把模型空间下的方向转换为切线空间下
                TANGENT_SPACE_ROTATION; 

                //切线空间下，平行光的方向
                o.lightDir = mul(rotation, ObjSpaceLightDir(v.vertex)); 

                return o;
            }

            // 要把所有跟法线方向有关的运算，都放到切线空间下。因为从法线贴图中取得的法线方向是在切线空间下的。
            fixed4 frag(v2f i) : SV_Target 
            {
                /**************************法线贴图处理******************************/
                //法线方向。从法线贴图中获取。法线贴图的颜色值 --> 法线方向
                fixed4 normalColor = tex2D(_NormalMap, i.uv.zw); // 在法线贴图中的颜色值

                fixed3 tangentNormal = UnpackNormal(normalColor); // 使用Unity内置的方法，从颜色值得到法线在切线空间的方向
                tangentNormal.xy = tangentNormal.xy * _BumpScale; // 控制凹凸程度
                tangentNormal = normalize(tangentNormal);

                //切线空间下的光照方向归一化
                fixed3 lightDir = normalize(i.lightDir);
                
                //兰伯特
                fixed3 diffuse = _LightColor0.rgb * _Color.rgb *saturate(dot(tangentNormal, lightDir)) ; // 颜色融合用乘法
                /********************************************************/

                /**************************灯光照明******************************/
                //获取场景光
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
                /********************************************************/

                /**************************贴图颜色******************************/
                //纹理坐标对应的纹理图片上的点的颜色
                fixed3 texColor = tex2D(_MainTex, i.uv) ;
                /********************************************************/

                //最终颜色叠加
                fixed3 color = (diffuse + ambient)*texColor;

                /**************************积雪效果******************************/
                //得到世界坐标系下的真正法向量（而非凹凸贴图产生的法向量）和雪落
                //下相反方向的点乘结果，即两者余弦值，并和_Snow（积雪程度）比较
                //把法线的从切线空间转到世界空间
                float3 worldNormal = float3(dot(i.tangentToWorld1,tangentNormal),dot(i.tangentToWorld2,tangentNormal),dot(i.tangentToWorld3,tangentNormal));

                //此处我们可以看出_Snow参数只是一个插值项，当上述夹角余弦值大于
                //lerp(1,-1,_Snow)=1-2*_Snow时，即表示此处积雪覆盖，所以此值越大，
                //积雪程度程度越大。此时给覆盖积雪的区域填充雪的颜色
                half difference=dot(worldNormal , _SnowDirection.xyz)-lerp(1,-1,_Snow);
                difference=saturate(difference);

                //边缘颜色渐变
                color=difference*_SnowColor.rgb+(1-difference)*color;
                /********************************************************/
                
                return fixed4(color, 1); //色彩叠加后与贴图颜色相乘
            }

            ENDCG
        }
        
    }
    FallBack "Diffuse"
}
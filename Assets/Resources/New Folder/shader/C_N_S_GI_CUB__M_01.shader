// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "shader//C_S_M_01"
{
	Properties 
	{
_main("_main", 2D) = "black" {}
_bump("_bump", 2D) = "black" {}
_Gloss("_Gloss", Range(0,1) ) = 0.563
_specular("_specular", Color) = (1,1,1,1)
_illum_C("_illum_C", 2D) = "black" {}
_illum_P("_illum_P", Range(0,1) ) = 0.197
_illum_Co("_illum_Co", Color) = (1,1,1,1)
_cube_C("_cube_C", Cube) = "black" {}

	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Geometry"
"IgnoreProjector"="False"
"RenderType"="Opaque"

		}

		
Cull Back
ZWrite On
ZTest LEqual
ColorMask RGBA
Fog{
}


		CGPROGRAM
#pragma surface surf BlinnPhongEditor  vertex:vert
#pragma target 2.0


sampler2D _main;
sampler2D _bump;
float _Gloss;
float4 _specular;
sampler2D _illum_C;
float _illum_P;
float4 _illum_Co;
samplerCUBE _cube_C;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
half3 spec = light.a * s.Gloss;
half4 c;
c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
c.a = s.Alpha;
return c;

			}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance (_LightColor0.rgb);
				res *= atten * 2.0;

				return LightingBlinnPhongEditor_PrePass( s, res );
			}
			
			struct Input {
				float2 uv_main;
float2 uv_bump;
float2 uv_illum_C;
float3 sWorldNormal;

			};

			void vert (inout appdata_full v, out Input o) {
float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);

o.sWorldNormal = mul((float3x3)unity_ObjectToWorld, SCALED_NORMAL);

			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Alpha = 1.0;
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				
float4 Tex2D0=tex2D(_main,(IN.uv_main.xyxy).xy);
float4 Tex2DNormal0=float4(UnpackNormal( tex2D(_bump,(IN.uv_bump.xyxy).xy)).xyz, 1.0 );
float4 Tex2D1=tex2D(_illum_C,(IN.uv_illum_C.xyxy).xy);
float4 Multiply1=_illum_Co * Tex2D1.aaaa;
float4 TexCUBE0=texCUBE(_cube_C,float4( IN.sWorldNormal.x, IN.sWorldNormal.y,IN.sWorldNormal.z,1.0 ));
float4 Add0=Multiply1 + TexCUBE0;
float4 Multiply0=Add0 * _illum_P.xxxx;
float4 Master0_5_NoInput = float4(1,1,1,1);
float4 Master0_7_NoInput = float4(0,0,0,0);
float4 Master0_6_NoInput = float4(1,1,1,1);
o.Albedo = Tex2D0;
o.Normal = Tex2DNormal0;
o.Emission = Multiply0;
o.Specular = _Gloss.xxxx;
o.Gloss = _specular;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback "Diffuse"
}
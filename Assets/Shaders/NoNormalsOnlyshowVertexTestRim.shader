  Shader "Custom/No Normals 4 Vertex Color Only Vertex Rim" {
    Properties {
     _MainTex ("Texture", 2D) = "white" {}
	_LightCutoff("Maximum distance", Float) = 2.0
	  _Color ("Main Color", Color) = (1,1,1,1)
	   _BumpMap ("Bumpmap", 2D) = "bump" {}
	  _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
      _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
     
      CGPROGRAM		
      	#pragma surface surf WrapLambert fullforwardshadows
		
		uniform float _LightCutoff;
		uniform float4 _Color;
		
      	half4 LightingWrapLambert (SurfaceOutput s, half3 lightDir, half atten) {
          half NdotL = dot (s.Normal, lightDir);

		  	atten = step(_LightCutoff, atten) * atten;
          	half4 c;
          	c.rgb = s.Albedo * _LightColor0.rgb * atten;
          	c.a = s.Alpha;
          	return c;
      }

      struct Input {
          float2 uv_MainTex;
          float4 color : COLOR; //vertex color
           float3 viewDir;
		 float2 uv_BumpMap;
      };
       		
	  
      sampler2D _MainTex;
       float4 _RimColor;
      float _RimPower;
      sampler2D _BumpMap;
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb *  IN.color.rgb * _Color;
           o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
          half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          o.Emission = _RimColor.rgb * pow (rim, _RimPower);
      }
      ENDCG
    }
    Fallback "Diffuse"
  }
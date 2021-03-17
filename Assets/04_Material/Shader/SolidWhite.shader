Shader "Solid White"{
	Properties{
		_Color("Color", COLOR) = (1,1,1,1)
		_MainTex ("Main_Texture", 2D) = "white"{}
		_SubTex("Sub_Texture", 2D) = "white"{}
	}
	SubShader{
		Pass{
			ZTest always
			Stencil{
				Ref 1
				Comp Equal
			}
			SetTexture [_SubTex]{
				Combine texture
			}
		}
		Pass{
			ZTest Less

			SetTexture[_MainTex]{
				Combine texture
			}
		}
	}
}
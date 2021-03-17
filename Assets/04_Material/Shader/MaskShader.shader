Shader "MaskShader"{
	Properties{
		_Color ("Color", COLOR) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white"{}
	}
	SubShader{
		ZWrite On
		Pass{
			Blend SrcAlpha OneMinusSrcAlpha
			Stencil{
				Ref 0
				Comp Equal
				Pass IncrSat
				ZFail zero
			}
			SetTexture[_MainTex]{
				ConstantColor [_Color]
				Combine texture, constant
			}
		}
	}
}
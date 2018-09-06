	<Texture>
		Des=针对ARGB_POT图片，设置她的Sprite_Atlas，走ETC2路线，压缩率4倍 内存等于原来的1/4
		<ArgbPotTextureFilter>
				Path=Assets/UIResources/OtherTexture/
		</ArgbPotTextureFilter>
		<ArgbPotTexturteRule>
			// 默认走ETC2路线，Read/Write不开启/设置Sprite_Atlas名字 名字加前缀tex_
		</ArgbPotTexturteRule>
	</Texture>
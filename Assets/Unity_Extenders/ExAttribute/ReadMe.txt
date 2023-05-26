2023 4/11 最終更新

＜概要＞
ExAttribute
変数のための新しい属性を追加する。
[OnValiable("変数名の上に重ねたい文字列")]
をシリアライズ属性かパブリックな変数につけることによって変数名に文字列を上書きできる。
例
[SerializeField, OnValiable("文字列")] int hoge;
[OnValiable("文字列")] public int huga;

＜歴史＞
これを作った後に[InspectorName("???")]で同じようなことができると知ったので、更に新しい機能として、
[OnValiable("変数名の上に重ねたい文字列, OncolorState.???")]の？にredやblueなどを
指定することでインスペクターの表示色を変更できる。
Color型を引数にすればよいとふつう思うのだがコンパイルエラーがでる。
興味がある人は編集したりGPT君に聞いて改良してみてほしい。

# 内容
Meshの頂点をランダムで選択し、その頂点上にGameObjectをInstantiateするプログラムです。

![イメージGif](https://i.gyazo.com/4d487cfb265d2f891002c46cd27e77ab.gif)

このGifは、SphereのMeshの頂点をランダムで選択し、SphereをInstantiateしています。

# 使い方
1. MeshFilterを持つGameObjectに `GenerationOnMeshVertices.cs`をアタッチする

![GenerationOnMeshVertices.csのアタッチ](https://user-images.githubusercontent.com/22683112/121015357-dfb3dc00-c7d5-11eb-9067-a4ea3579969d.png)

2. 変数を設定する
- Gneration Objs
生成するGameObjectを格納する配列です。
これは配列になっており、複数のGameOjbectをランダムでInstantiateできます。

- Generation Obj Ratios
生成頻度の比率を格納する配列です。
配列の要素数を必ず上記の Generation Objs と同じにしてください。
それぞれのGameObjectの生成の比率を設定します。

![GenerationObjRatios](https://user-images.githubusercontent.com/22683112/121029965-024cf180-c7e4-11eb-8a43-954dab464d70.png)

添付画像の生成の比率は、Sphere : Cube : Capsule = 1 : 2 : 3 になります。

- Trans Range
頂点座標からどの程度まで離れるかを格納します。
ex) Trans Range = 0.5の時、 選択された頂点座標から、-0.5~0.5の範囲内で生成するオブジェクトの位置が移動します。
この辺は、コードをみながら、臨機応変に変えてください。

- Num Of Init Generation
初期に配置するGameObjectの数です。

- Min Generation Time, Max Generation Time
1度生成してから、次に生成するまでにかかる時間の範囲を格納します。
ex) Min Generation Time = 1, Max Generation Time = 3
次に生成するのは、1~3秒でランダムに選択される。

- Min Num Of Gneration, Max Num Of Generation
1度の生成にどのくらいのObjectを生成するかを格納する変数。
ex) Min Num Of Generation = 1, Max Num Of Generation = 3
1度に生成するのは、1~3個でランダムに選択された個数のGameObjectである。

- Upper Limit Of Generation
生成するGameObjectの数の上限を格納する変数
ランダムに選択された頂点は、それ以降は選択されないように、削除しているので、Meshの頂点より少ない数に設定してください。
これも、コードをみながら、臨機応変に変更してください。

# 最後に
バグや要望がありましたら、Issuesに記入してください。
時間がある時に、見ます。

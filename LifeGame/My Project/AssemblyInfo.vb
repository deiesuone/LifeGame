﻿Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports System.Resources
Imports System.Windows

' アセンブリに関する一般情報は以下の属性セットをとおして制御されます。
' アセンブリに関連付けられている情報を変更するには、
' これらの属性値を変更してください。

' アセンブリ属性の値を確認します。

<Assembly: AssemblyTitle("LifeGame")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("DS1")> 
<Assembly: AssemblyProduct("LifeGame")> 
<Assembly: AssemblyCopyright("")> 
<Assembly: AssemblyTrademark("")> 
<Assembly: ComVisible(false)>

'ローカライズ可能なアプリケーションのビルドを開始するには、
'.vbproj ファイルの <UICulture>CultureYouAreCodingWith</UICulture> を
'<PropertyGroup> 内に設定します。たとえば、
'ソース ファイルで英語 (米国) を使用している場合、<UICulture> を "en-US" に設定します。次に、
'下の NeutralResourceLanguage 属性のコメントを解除し、下の行の "en-US" を
'プロジェクト ファイルの UICulture 設定と一致するように更新します。

'<Assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)> 


'ThemeInfo 属性は、テーマ固有および汎用のリソース ディクショナリがある場所を表します。
'第 1 パラメーター: テーマ固有のリソース ディクショナリが置かれている場所
'(リソースがページ、
' またはアプリケーション リソース ディクショナリに見つからない場合に使用されます)

'第 2 パラメーター: 汎用リソース ディクショナリが置かれている場所
'(リソースがページ、
'アプリケーション、テーマ固有のリソース ディクショナリに見つからない場合に使用されます)
<Assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)>



'このプロジェクトが COM に公開される場合、次の GUID がタイプ ライブラリの ID になります。
<Assembly: Guid("0f389b68-6ed7-40e6-8c4c-93f426d11c9b")> 

' アセンブリのバージョン情報は、以下の 4 つの値で構成されています:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' すべての値を指定するか、下のように '*' を使ってビルドおよびリビジョン番号を 
' 既定値にすることができます:
' <Assembly: AssemblyVersion("1.0.*")> 

<Assembly: AssemblyVersion("1.0.0.0")> 
<Assembly: AssemblyFileVersion("1.0.0.0")> 

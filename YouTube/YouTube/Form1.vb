Imports System.Xml
Imports System.Net
Imports System.IO

Public Class Form1
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' 神戸電子　殿様バッタ編
        WebBrowser1.Navigate("http://www.youtube.com/v/ypuWyPRRy9E&autoplay=1")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ' ETロボコン2011 CS大会 インコース転倒シーン
        WebBrowser1.Navigate("http://www.youtube.com/v/p7pIJ8UdMR8&autoplay=1")
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ' AKB48 ヘビーローテーション
        WebBrowser1.Navigate("http://www.youtube.com/v/lkHlnWFnA0c&autoplay=1")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        ' One World Baton Relay in JAPAN 2012 兵庫県 神戸電子専門学校にて
        WebBrowser1.Navigate("http://www.youtube.com/v/QUgmNphhuqg&autoplay=1")
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ' 入力されたキーワードによりYoutube動画を検索し、表示させる
        Dim videoID As String = "ypuWyPRRy9E"
        Dim keyword As String = "神戸電子専門学校 殿様バッタ"

        If TextBox1.Text <> "" Or TextBox1.Text <> Nothing Then
            ' ここでテキストボックスに入力されたキーワードを取得する
            keyword = TextBox1.Text
        End If

        ' キーワードを使ってYoutubeから動画を1件検索
        Dim search As String = "http://gdata.youtube.com/feeds/api/videos?vq=" + keyword + "&orderby=relevance_lang_ja&max-results=1"
        Dim XML As String = GetXML(search)

        ' XMLデータを取り扱うための準備。ルート要素の取得
        Dim Doc As New XmlDocument
        Doc.LoadXml(XML)
        Dim root As XmlNode = Doc.DocumentElement

        ' Add the namespace.（名前空間）
        Dim nsmgr As New XmlNamespaceManager(Doc.NameTable)
        nsmgr.AddNamespace("ns", "http://www.w3.org/2005/Atom")

        ' URLを取り出す
        Dim node As XmlNode = root.SelectSingleNode("descendant::ns:entry/ns:id", nsmgr)
        videoID = node.InnerXml

        ' 余分なデータを除いたIDを取り出す
        Dim videoParam As String = videoID.Substring(videoID.LastIndexOf("/") + 1)

        ' 動画用URLを生成する
        Dim videoURL As String = "http://www.youtube.com/v/" + videoParam + "&autoplay=1"
        ' MessageBox.Show(videoURL)

        ' ここで画面に表示する
        WebBrowser1.Navigate(videoURL)

    End Sub

    Function GetXML(ByVal URL As String) As String
        Dim Request As HttpWebRequest = HttpWebRequest.Create(URL)
        Dim sr As New StreamReader(Request.GetResponse.GetResponseStream)

        Request.Method = "POST"
        GetXML = sr.ReadToEnd
        sr.Close()
        Request.GetResponse.Close()
    End Function

    Private Sub ClearImage() Handles Button1.Click, Button2.Click, Button3.Click, Button4.Click, Button5.Click
        'PictureBox1.Visible = False
        Panel2.Visible = False
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        On Error Resume Next

        If e.KeyChar = Chr(Keys.Enter) Then
            Call Button5_Click(sender, e)
            Panel2.Visible = False
        End If
    End Sub
End Class

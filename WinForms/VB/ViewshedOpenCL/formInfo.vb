Imports System.Windows.Forms

Namespace ViewshedOpenCL

    Public Class frmInfo

        Public Function Execute(_owner As IWin32Window, _title As String, _text As String) As DialogResult
            Me.Text = _title
            txtbxInfo.Text = _text
            Return Me.ShowDialog(_owner)
        End Function

    End Class

End Namespace

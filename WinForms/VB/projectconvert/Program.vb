Imports System
Imports TatukGIS.NDK
Imports System.Drawing
Imports TatukGIS.RTL
Imports TatukGIS.NDK.WinForms
Imports System.IO

Namespace projectconvert

    Class Program

        Public Shared vwr As TGIS_ViewerBmp

        Public Shared str As String

        Shared Sub Main(ByVal args() As String)
            Console.WriteLine("TatukGIS Samples - TTKGP->TTKPROJECT converter")
            If (args.Length < 1) Then
                Console.WriteLine("Usage : ")
                Console.WriteLine("Enter path of the TTKGP project. TTKPROJECT output will be placed in the same directory.")
                Console.WriteLine("TTKGP file will be kept in its place after conversion.")
                Console.WriteLine("Put directories with filenames and .TTKGP extension into parameters.")
                Return
            End If

            vwr = New TGIS_ViewerBmp
            str = args(0)
            vwr.Open(str)
            str = Path.ChangeExtension(str, ".ttkproject")
            vwr.SaveProjectAs(str)
        End Sub
    End Class
End Namespace
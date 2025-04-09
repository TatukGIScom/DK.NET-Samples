Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace any2any
    ''' <summary>
    ''' Summary description for Class.
    ''' </summary>
    Friend Class [Class]
        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main(ByVal args As String())
            Dim lm As TGIS_LayerVector
            Dim ll As TGIS_LayerVector
            Dim shape_type As TGIS_ShapeType

            Console.WriteLine("TatukGIS Samples - ANY->ANY converter ( Vector files only )")
            If args.Length <> 3 Then
                Console.WriteLine("Usage: ANY2SQL source_file destination shape_type")
                Console.WriteLine("Where shape_type:")
                Console.WriteLine(" A - Arc")
                Console.WriteLine(" G - polyGon")
                Console.WriteLine(" P - Point")
                Console.WriteLine(" M - Multipoint")
                Return
            End If

            Try
                lm = CType(TGIS_Utils.GisCreateLayer("", args(0)), TGIS_LayerVector)
                If lm Is Nothing Then
                    Console.WriteLine(String.Format("### ERROR: File {0} not found", args(0)))
                    Return
                End If
                lm.Open()

                Select Case args(2).Chars(0)
                    Case "A"c
                        shape_type = TGIS_ShapeType.Arc
                    Case "G"c
                        shape_type = TGIS_ShapeType.Polygon
                    Case "P"c
                        shape_type = TGIS_ShapeType.Point
                    Case "M"c
                        shape_type = TGIS_ShapeType.MultiPoint
                    Case Else
                        shape_type = TGIS_ShapeType.Unknown
                End Select

                ll = CType(TGIS_Utils.GisCreateLayer("", args(1)), TGIS_LayerVector)

                If ll Is Nothing Then
                    Console.WriteLine(String.Format("### ERROR: File {0} not found", args(1)))
                    Return
                End If
                ll.ImportLayer(lm, lm.Extent, shape_type, "", False)
            Catch e As Exception
                Console.WriteLine(e.ToString())
            End Try
        End Sub
    End Class
End Namespace

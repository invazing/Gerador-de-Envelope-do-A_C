Imports FirebirdSql.Data.FirebirdClient
Public Class Main
    Dim conexaoFB As FbConnection
    Dim da As FbDataAdapter
    Dim ds As New DataSet
    Dim id_cliente, nome, social, endereco, bairro, telefone, celular, cidade As String
    Dim datacadastro As Date
    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawString(id_cliente + " - " + nome, New Font("Arial", 14, FontStyle.Regular), Brushes.Black, 10, 10)
        e.Graphics.DrawString("SOCIAL : " + social, New Font("Arial", 14, FontStyle.Regular), Brushes.Black, 10, 40)
        e.Graphics.DrawString("END : " + endereco, New Font("Arial", 14, FontStyle.Regular), Brushes.Black, 10, 70)
        e.Graphics.DrawString("BAIRRO : " + bairro, New Font("Arial", 14, FontStyle.Regular), Brushes.Black, 10, 100)
        e.Graphics.DrawString("FONE : " + telefone, New Font("Arial", 14, FontStyle.Regular), Brushes.Black, 10, 130)
        e.Graphics.DrawString("FONE : " + celular, New Font("Arial", 14, FontStyle.Regular), Brushes.Black, 10, 160)
        e.Graphics.DrawString("CIDADE : " + cidade, New Font("Arial", 14, FontStyle.Regular), Brushes.Black, 10, 190)
        e.Graphics.DrawString("DATA CADASTRO : " + datacadastro, New Font("Arial", 14, FontStyle.Regular), Brushes.Black, 10, 220)
    End Sub
    Private Sub Cobranca_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown, cod_cliente.KeyDown
        If e.KeyCode = Keys.Enter Then
            conexaoFB.Open()
            Try
                da = New FbDataAdapter("SELECT GE_pessoa.id_pessoa AS COD_CLIENTE, GE_PESSOA.razao AS NOME, GE_PESSOA.fantasia AS SOCIAL, GE_PESSOA.data_cadastro AS CADASTRO, GE_PESSOA_ENDERECO.endereco AS ENDERECO, GE_PESSOA_ENDERECO.bairro AS BAIRRO, GE_PESSOA_ENDERECO.telefone AS TELEFONE, GE_PESSOA_ENDERECO.celular AS CELULAR, GE_CIDADE.nome AS CIDADE FROM GE_PESSOA INNER JOIN GE_PESSOA_ENDERECO ON GE_PESSOA_ENDERECO.id_pessoa = GE_PESSOA.id_pessoa INNER JOIN GE_CIDADE ON GE_CIDADE.id_cidade = GE_PESSOA_ENDERECO.id_cidade WHERE GE_PESSOA.id_pessoa='" & cod_cliente.Text & "'", conexaoFB)
                da.Fill(ds, "Cadastro")
                Dim dt As DataTable = New DataTable
                da.Fill(dt)
                id_cliente = dt.Rows(0)(0).ToString
                nome = dt.Rows(0)(1).ToString
                social = dt.Rows(0)(2).ToString
                datacadastro = dt.Rows(0)(3).ToString
                endereco = dt.Rows(0)(4).ToString
                bairro = dt.Rows(0)(5).ToString
                telefone = dt.Rows(0)(6).ToString
                celular = dt.Rows(0)(7).ToString
                cidade = dt.Rows(0)(8).ToString
                PrintDocument1.Print()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            cod_cliente.Text = ""
            conexaoFB.Close()
        End If
    End Sub
    Private Sub Cobranca_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conexaoFB = New FbConnection("Server=localhost;User=SYSDBA;Password=masterkey;Database=C:\NTS\SIGEWIN\SIGE.FDB")
    End Sub
End Class

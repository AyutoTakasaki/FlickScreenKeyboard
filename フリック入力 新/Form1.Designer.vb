<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.FlickNaviPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.FlickNaviPanelPanel = New System.Windows.Forms.Panel()
        Me.ButtonTable = New System.Windows.Forms.TableLayoutPanel()
        Me.SameKey = New System.Windows.Forms.Timer(Me.components)
        Me.FlickNaviPanelPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'FlickNaviPanel
        '
        Me.FlickNaviPanel.BackColor = System.Drawing.Color.White
        Me.FlickNaviPanel.ColumnCount = 3
        Me.FlickNaviPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.FlickNaviPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.FlickNaviPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.FlickNaviPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlickNaviPanel.Location = New System.Drawing.Point(0, 0)
        Me.FlickNaviPanel.Name = "FlickNaviPanel"
        Me.FlickNaviPanel.RowCount = 3
        Me.FlickNaviPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.FlickNaviPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.FlickNaviPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.FlickNaviPanel.Size = New System.Drawing.Size(81, 81)
        Me.FlickNaviPanel.TabIndex = 2
        '
        'FlickNaviPanelPanel
        '
        Me.FlickNaviPanelPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.FlickNaviPanelPanel.Controls.Add(Me.FlickNaviPanel)
        Me.FlickNaviPanelPanel.Location = New System.Drawing.Point(12, 123)
        Me.FlickNaviPanelPanel.Name = "FlickNaviPanelPanel"
        Me.FlickNaviPanelPanel.Size = New System.Drawing.Size(85, 85)
        Me.FlickNaviPanelPanel.TabIndex = 3
        Me.FlickNaviPanelPanel.Visible = False
        '
        'ButtonTable
        '
        Me.ButtonTable.ColumnCount = 5
        Me.ButtonTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.ButtonTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.ButtonTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.ButtonTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.ButtonTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.ButtonTable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonTable.Location = New System.Drawing.Point(0, 0)
        Me.ButtonTable.Name = "ButtonTable"
        Me.ButtonTable.RowCount = 4
        Me.ButtonTable.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.ButtonTable.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.ButtonTable.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.ButtonTable.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.ButtonTable.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.ButtonTable.Size = New System.Drawing.Size(316, 234)
        Me.ButtonTable.TabIndex = 1
        '
        'SameKey
        '
        Me.SameKey.Enabled = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(316, 234)
        Me.Controls.Add(Me.FlickNaviPanelPanel)
        Me.Controls.Add(Me.ButtonTable)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "Form1"
        Me.Opacity = 0.95R
        Me.FlickNaviPanelPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents FlickNaviPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents FlickNaviPanelPanel As System.Windows.Forms.Panel
    Friend WithEvents ButtonTable As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents SameKey As System.Windows.Forms.Timer

End Class

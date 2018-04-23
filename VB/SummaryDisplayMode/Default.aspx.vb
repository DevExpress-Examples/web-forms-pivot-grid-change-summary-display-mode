Imports System
Imports System.Collections.Generic
Imports DevExpress.Web.ASPxPivotGrid
Imports DevExpress.Data.PivotGrid

Namespace SummaryDisplayMode
    Partial Public Class DefaultForm
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not IsPostBack AndAlso Not IsCallback Then
                SetSelectedConfiguration()
            End If
        End Sub
        Private Const SummaryDisplayTypeDataFieldID As String = "summaryDisplayTypeDataField"

        Private Enum SummaryDisplayTypeGroup
            Variation = 0
            Percentage = 1
            Rank = 2
            Index = 3
        End Enum

        Private ReadOnly Property SelectedGroup() As SummaryDisplayTypeGroup
            Get
                Return CType(rgSummaryDisplayTypeGroups.SelectedIndex, SummaryDisplayTypeGroup)
            End Get
        End Property
        Private Property SourceDataFieldName() As String
            Get
                Return cachedSourceDataFieldName.Value
            End Get
            Set(ByVal value As String)
                cachedSourceDataFieldName.Value = value
            End Set
        End Property
        Private ReadOnly Property SourceDataField() As PivotGridField
            Get
                Return pivotGrid.Fields(SourceDataFieldName)
            End Get
        End Property
        Private ReadOnly Property SummaryDisplayTypeDataField() As PivotGridField
            Get
                Return pivotGrid.Fields(SummaryDisplayTypeDataFieldID)
            End Get
        End Property
        Protected Sub rgSummaryDisplayTypeGroups_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectedConfiguration()
        End Sub
        Private Sub SetSelectedConfiguration()
            SetSelectedGroup()
            SetSelectedSummaryDisplayType()
        End Sub
        Private Sub SetSelectedGroup()
            ConfigurePivotGridLayout(SelectedGroup)
            Dim isVariation As Boolean = (SelectedGroup = SummaryDisplayTypeGroup.Variation)
            cbAllowCrossGroupVariation.Visible = isVariation
            cbHideEmptyVariationItems.Visible = isVariation
            If SourceDataField IsNot Nothing Then
                cbShowRawValues.Checked = SourceDataField.Visible
            End If
            ConfigureSummaryDisplayTypeComboBox(SelectedGroup)
        End Sub
        Private Sub ConfigurePivotGridLayout(ByVal typeGroup As SummaryDisplayTypeGroup)
            pivotGrid.BeginUpdate()
            Select Case typeGroup
                Case SummaryDisplayTypeGroup.Variation
                        pivotGrid.DataSourceID = "SalesPersonsDataSource"
                        pivotGrid.Fields.Clear()
                        Dim fieldYear As PivotGridField = pivotGrid.Fields.Add("OrderDate", DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
                        fieldYear.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear
                        fieldYear.Caption = "Year"
                        Dim fieldQuarter As PivotGridField = pivotGrid.Fields.Add("OrderDate", DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
                        fieldQuarter.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateQuarter
                        fieldQuarter.ValueFormat.FormatString = "Qtr {0}"
                        fieldQuarter.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom
                        fieldQuarter.Caption = "Quarter"
                        pivotGrid.Fields.Add("Sales Person", DevExpress.XtraPivotGrid.PivotArea.RowArea)

                        Dim sourceDataField_Renamed As PivotGridField = pivotGrid.Fields.Add("OrderID", DevExpress.XtraPivotGrid.PivotArea.DataArea)
                        sourceDataField_Renamed.SummaryType = PivotSummaryType.Count
                        sourceDataField_Renamed.Caption = "Order Count"
                        SourceDataFieldName = sourceDataField_Renamed.FieldName

                        Dim summaryDisplayTypeDataField_Renamed As PivotGridField = pivotGrid.Fields.Add(SourceDataFieldName, DevExpress.XtraPivotGrid.PivotArea.DataArea)
                        summaryDisplayTypeDataField_Renamed.SummaryType = sourceDataField_Renamed.SummaryType
                        summaryDisplayTypeDataField_Renamed.ID = SummaryDisplayTypeDataFieldID
                Case SummaryDisplayTypeGroup.Percentage
                        pivotGrid.DataSourceID = "ProductReportsDataSource"
                        pivotGrid.Fields.Clear()
                        Dim fieldYear As PivotGridField = pivotGrid.Fields.Add("ShippedDate", DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
                        fieldYear.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear
                        fieldYear.Caption = "Year"
                        Dim fieldMonth As PivotGridField = pivotGrid.Fields.Add("ShippedDate", DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
                        fieldMonth.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth
                        fieldMonth.Caption = "Month"
                        pivotGrid.Fields.Add("CategoryName", DevExpress.XtraPivotGrid.PivotArea.RowArea)
                        pivotGrid.Fields.Add("ProductName", DevExpress.XtraPivotGrid.PivotArea.RowArea)

                        Dim sourceDataField_Renamed As PivotGridField = pivotGrid.Fields.Add("ProductSales", DevExpress.XtraPivotGrid.PivotArea.DataArea)
                        SourceDataFieldName = sourceDataField_Renamed.FieldName

                        Dim summaryDisplayTypeDataField_Renamed As PivotGridField = pivotGrid.Fields.Add(SourceDataFieldName, DevExpress.XtraPivotGrid.PivotArea.DataArea)
                        summaryDisplayTypeDataField_Renamed.ID = SummaryDisplayTypeDataFieldID
                Case SummaryDisplayTypeGroup.Rank
                        pivotGrid.DataSourceID = "SalesPersonsDataSource"
                        pivotGrid.Fields.Clear()
                        Dim fieldYear As PivotGridField = pivotGrid.Fields.Add("OrderDate", DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
                        fieldYear.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear
                        fieldYear.Caption = "Year"
                        Dim fieldQuarter As PivotGridField = pivotGrid.Fields.Add("OrderDate", DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
                        fieldQuarter.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateQuarter
                        fieldQuarter.ValueFormat.FormatString = "Qtr {0}"
                        fieldQuarter.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom
                        fieldQuarter.Caption = "Quarter"
                        pivotGrid.Fields.Add("Country", DevExpress.XtraPivotGrid.PivotArea.RowArea)
                        pivotGrid.Fields.Add("Sales Person", DevExpress.XtraPivotGrid.PivotArea.RowArea)

                        Dim sourceDataField_Renamed As PivotGridField = pivotGrid.Fields.Add("Extended_Price", DevExpress.XtraPivotGrid.PivotArea.DataArea)
                        sourceDataField_Renamed.Caption = "Sales"
                        SourceDataFieldName = sourceDataField_Renamed.FieldName

                        Dim summaryDisplayTypeDataField_Renamed As PivotGridField = pivotGrid.Fields.Add(SourceDataFieldName, DevExpress.XtraPivotGrid.PivotArea.DataArea)
                        summaryDisplayTypeDataField_Renamed.ID = SummaryDisplayTypeDataFieldID
                Case SummaryDisplayTypeGroup.Index
                        pivotGrid.DataSourceID = "SalesPersonsDataSource"
                        pivotGrid.Fields.Clear()
                        Dim fieldYear As PivotGridField = pivotGrid.Fields.Add("Country", DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
                        pivotGrid.Fields.Add("CategoryName", DevExpress.XtraPivotGrid.PivotArea.RowArea)

                        Dim sourceDataField_Renamed As PivotGridField = pivotGrid.Fields.Add("Extended_Price", DevExpress.XtraPivotGrid.PivotArea.DataArea)
                        sourceDataField_Renamed.Caption = "Sales"
                        SourceDataFieldName = sourceDataField_Renamed.FieldName

                        Dim summaryDisplayTypeDataField_Renamed As PivotGridField = pivotGrid.Fields.Add(SourceDataFieldName, DevExpress.XtraPivotGrid.PivotArea.DataArea)
                        summaryDisplayTypeDataField_Renamed.ID = SummaryDisplayTypeDataFieldID
            End Select
            pivotGrid.EndUpdate()
            pivotGrid.DataBind()
        End Sub
        Private Sub ConfigureSummaryDisplayTypeComboBox(ByVal typeGroup As SummaryDisplayTypeGroup)
            Dim types As New List(Of PivotSummaryDisplayType)()
            Select Case typeGroup
                Case SummaryDisplayTypeGroup.Variation
                    types.Add(PivotSummaryDisplayType.AbsoluteVariation)
                    types.Add(PivotSummaryDisplayType.PercentVariation)
                Case SummaryDisplayTypeGroup.Percentage
                    types.Add(PivotSummaryDisplayType.PercentOfColumn)
                    types.Add(PivotSummaryDisplayType.PercentOfRow)
                    types.Add(PivotSummaryDisplayType.PercentOfColumnGrandTotal)
                    types.Add(PivotSummaryDisplayType.PercentOfRowGrandTotal)
                    types.Add(PivotSummaryDisplayType.PercentOfGrandTotal)
                Case SummaryDisplayTypeGroup.Rank
                    types.Add(PivotSummaryDisplayType.RankInColumnLargestToSmallest)
                    types.Add(PivotSummaryDisplayType.RankInColumnSmallestToLargest)
                    types.Add(PivotSummaryDisplayType.RankInRowLargestToSmallest)
                    types.Add(PivotSummaryDisplayType.RankInRowSmallestToLargest)
                Case SummaryDisplayTypeGroup.Index
                    types.Add(PivotSummaryDisplayType.Index)
            End Select
            ddlSummaryDisplayType.Items.Clear()
            For Each type As PivotSummaryDisplayType In types
                ddlSummaryDisplayType.Items.Add(System.Enum.GetName(GetType(PivotSummaryDisplayType), type), type)
            Next type
            ddlSummaryDisplayType.SelectedIndex = 0
        End Sub
        Protected Sub cbShowRawValues_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            If SourceDataFieldName IsNot Nothing Then
                SourceDataField.Visible = cbShowRawValues.Checked
                If SourceDataField.Visible Then
                    SourceDataField.AreaIndex = 0
                End If
            End If
        End Sub
        Protected Sub cbHideEmptyVariationItems_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            If SummaryDisplayTypeDataField IsNot Nothing Then
                SummaryDisplayTypeDataField.Options.HideEmptyVariationItems = cbHideEmptyVariationItems.Checked
            End If
        End Sub
        Protected Sub cbAllowCrossGroupVariation_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            pivotGrid.OptionsData.AllowCrossGroupVariation = cbAllowCrossGroupVariation.Checked
        End Sub
        Protected Sub ddlSummaryDisplayType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectedSummaryDisplayType()
        End Sub
        Private Sub SetSelectedSummaryDisplayType()
            If SummaryDisplayTypeDataField Is Nothing Then
                Return
            End If
            SummaryDisplayTypeDataField.SummaryDisplayType = DirectCast(System.Enum.Parse(GetType(PivotSummaryDisplayType), ddlSummaryDisplayType.SelectedItem.Text), PivotSummaryDisplayType)
            SummaryDisplayTypeDataField.Caption = String.Format("{0}", SummaryDisplayTypeDataField.SummaryDisplayType)
        End Sub

    End Class
End Namespace
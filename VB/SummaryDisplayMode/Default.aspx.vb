Imports System
Imports System.Collections.Generic
Imports DevExpress.Web.ASPxPivotGrid
Imports DevExpress.Data.PivotGrid
Imports DevExpress.XtraPivotGrid

Namespace SummaryDisplayMode
    Partial Public Class DefaultForm
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not IsPostBack AndAlso Not IsCallback Then
                SetSelectedConfiguration()
            End If
        End Sub
        Private Const SummaryDisplayTypeDataFieldID As String = "summaryDisplayTypeDataField"
        Private Const SourceDataFieldID As String = "sourceDataField"

        Private Enum SummaryDisplayTypeGroup
            Variation = 0
            Percentage = 1
            Rank = 2
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
                    Dim fieldYear As PivotGridField = pivotGrid.Fields.AddDataSourceColumn("OrderDate", PivotArea.ColumnArea, PivotGroupInterval.DateYear)
                    fieldYear.Caption = "Year"
                    Dim fieldQuarter As PivotGridField = pivotGrid.Fields.AddDataSourceColumn("OrderDate", PivotArea.ColumnArea, PivotGroupInterval.DateQuarter)
                    fieldQuarter.ValueFormat.FormatString = "Qtr {0}"
                    fieldQuarter.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    fieldQuarter.Caption = "Quarter"
                    Dim salesPersonField As PivotGridField = pivotGrid.Fields.AddDataSourceColumn("Sales Person", PivotArea.RowArea)
                    SourceDataFieldName = "OrderID"
                    Dim sourceDataField As PivotGridField = pivotGrid.Fields.AddDataSourceColumn(SourceDataFieldName, PivotArea.DataArea)
                    sourceDataField.SummaryType = PivotSummaryType.Count
                    sourceDataField.Caption = "Order Count"
                    sourceDataField.ID = SourceDataFieldID
                    Dim summaryDisplayTypeDataField As PivotGridField = pivotGrid.Fields.AddDataSourceColumn(SourceDataFieldName, PivotArea.DataArea)
                    summaryDisplayTypeDataField.SummaryType = sourceDataField.SummaryType
                    summaryDisplayTypeDataField.ID = SummaryDisplayTypeDataFieldID
                Case SummaryDisplayTypeGroup.Percentage
                    pivotGrid.DataSourceID = "ProductReportsDataSource"
                    pivotGrid.Fields.Clear()
                    Dim fieldYear As PivotGridField = pivotGrid.Fields.AddDataSourceColumn("ShippedDate", PivotArea.ColumnArea, PivotGroupInterval.DateYear)
                    fieldYear.Caption = "Year"
                    Dim fieldMonth As PivotGridField = pivotGrid.Fields.AddDataSourceColumn("ShippedDate", PivotArea.ColumnArea, PivotGroupInterval.DateMonth)
                    fieldMonth.Caption = "Month"
                    Dim categoryNameField As PivotGridField = pivotGrid.Fields.AddDataSourceColumn("CategoryName", PivotArea.RowArea)
                    Dim productNameField As PivotGridField = pivotGrid.Fields.AddDataSourceColumn("ProductName", PivotArea.RowArea)
                    SourceDataFieldName = "ProductSales"
                    Dim sourceDataField As PivotGridField = pivotGrid.Fields.AddDataSourceColumn(SourceDataFieldName, PivotArea.DataArea)
                    sourceDataField.ID = SourceDataFieldID
                    Dim summaryDisplayTypeDataField As PivotGridField = pivotGrid.Fields.AddDataSourceColumn(SourceDataFieldName, PivotArea.DataArea)
                    summaryDisplayTypeDataField.ID = SummaryDisplayTypeDataFieldID
                Case SummaryDisplayTypeGroup.Rank
                    pivotGrid.DataSourceID = "SalesPersonsDataSource"
                    pivotGrid.Fields.Clear()
                    Dim fieldYear As PivotGridField = pivotGrid.Fields.AddDataSourceColumn("OrderDate", PivotArea.ColumnArea, PivotGroupInterval.DateYear)
                    fieldYear.Caption = "Year"
                    Dim fieldQuarter As PivotGridField = pivotGrid.Fields.AddDataSourceColumn("OrderDate", PivotArea.ColumnArea, PivotGroupInterval.DateQuarter)
                    fieldQuarter.ValueFormat.FormatString = "Qtr {0}"
                    fieldQuarter.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    fieldQuarter.Caption = "Quarter"
                    Dim countryField As PivotGridField = pivotGrid.Fields.AddDataSourceColumn("Country", PivotArea.RowArea)
                    Dim salesPersonField As PivotGridField = pivotGrid.Fields.AddDataSourceColumn("Sales Person", PivotArea.RowArea)
                    SourceDataFieldName = "Extended Price"
                    Dim sourceDataField As PivotGridField = pivotGrid.Fields.AddDataSourceColumn(SourceDataFieldName, PivotArea.DataArea)
                    sourceDataField.Caption = "Sales"
                    sourceDataField.ID = SourceDataFieldID
                    Dim summaryDisplayTypeDataField As PivotGridField = pivotGrid.Fields.AddDataSourceColumn(SourceDataFieldName, PivotArea.DataArea)
                    summaryDisplayTypeDataField.ID = SummaryDisplayTypeDataFieldID

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
            Dim sourceBinding As New DataSourceColumnBinding(SourceDataFieldName)
            Select Case ddlSummaryDisplayType.SelectedItem.Text
                Case "AbsoluteVariation"
                    SummaryDisplayTypeDataField.DataBinding = New DifferenceBinding(sourceBinding, CalculationPartitioningCriteria.RowValue, CalculationDirection.DownThenAcross, DifferenceTarget.Previous, DifferenceType.Absolute)
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "n0"
                Case "PercentVariation"
                    SummaryDisplayTypeDataField.DataBinding = New DifferenceBinding(sourceBinding, CalculationPartitioningCriteria.RowValue, CalculationDirection.DownThenAcross, DifferenceTarget.Previous, DifferenceType.Percentage)
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "p2"
                Case "PercentOfColumn"
                    SummaryDisplayTypeDataField.DataBinding = New PercentOfTotalBinding(sourceBinding, CalculationPartitioningCriteria.ColumnValueAndRowParentValue)
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "p2"
                Case "PercentOfRow"
                    SummaryDisplayTypeDataField.DataBinding = New PercentOfTotalBinding(sourceBinding, CalculationPartitioningCriteria.RowValueAndColumnParentValue)
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "p2"
                Case "PercentOfColumnGrandTotal"
                    SummaryDisplayTypeDataField.DataBinding = New PercentOfTotalBinding(sourceBinding, CalculationPartitioningCriteria.ColumnValue)
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "p2"

                Case "PercentOfRowGrandTotal"
                    SummaryDisplayTypeDataField.DataBinding = New PercentOfTotalBinding(sourceBinding, CalculationPartitioningCriteria.RowValue)
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "p2"
                Case "PercentOfGrandTotal"
                    SummaryDisplayTypeDataField.DataBinding = New PercentOfTotalBinding(sourceBinding, CalculationPartitioningCriteria.None)
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "p2"
                Case "RankInColumnLargestToSmallest"
                    SummaryDisplayTypeDataField.DataBinding = New RankBinding(sourceBinding, CalculationPartitioningCriteria.ColumnValue, RankType.Dense, PivotSortOrder.Descending)
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "n0"
                Case "RankInColumnSmallestToLargest"
                    SummaryDisplayTypeDataField.DataBinding = New RankBinding(sourceBinding, CalculationPartitioningCriteria.ColumnValue, RankType.Dense, PivotSortOrder.Ascending)
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "n0"
                Case "RankInRowLargestToSmallest"
                    SummaryDisplayTypeDataField.DataBinding = New RankBinding(sourceBinding, CalculationPartitioningCriteria.RowValue, RankType.Dense, PivotSortOrder.Descending)
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "n0"
                Case "RankInRowSmallestToLargest"
                    SummaryDisplayTypeDataField.DataBinding = New RankBinding(sourceBinding, CalculationPartitioningCriteria.ColumnValue, RankType.Dense, PivotSortOrder.Ascending)
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "n0"
            End Select
            SummaryDisplayTypeDataField.Caption = String.Format("{0}", ddlSummaryDisplayType.SelectedItem.Text)
        End Sub

    End Class
End Namespace
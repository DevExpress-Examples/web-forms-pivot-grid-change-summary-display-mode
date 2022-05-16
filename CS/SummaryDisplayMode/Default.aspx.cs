using System;
using System.Collections.Generic;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.Data.PivotGrid;
using DevExpress.XtraPivotGrid;

namespace SummaryDisplayMode {
    public partial class DefaultForm : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack && !IsCallback)
                SetSelectedConfiguration();
        }
        const string SummaryDisplayTypeDataFieldID = "summaryDisplayTypeDataField";
        const string SourceDataFieldID = "sourceDataField";


        enum SummaryDisplayTypeGroup { Variation = 0, Percentage = 1, Rank = 2};

        SummaryDisplayTypeGroup SelectedGroup {
            get { return (SummaryDisplayTypeGroup)(rgSummaryDisplayTypeGroups.SelectedIndex); }
        }
        string SourceDataFieldName {
            get { return cachedSourceDataFieldName.Value; }
            set { cachedSourceDataFieldName.Value = value; }
        }
        PivotGridField SourceDataField {
            get { return pivotGrid.Fields[SourceDataFieldID]; }
        }
        PivotGridField SummaryDisplayTypeDataField {
            get { return pivotGrid.Fields[SummaryDisplayTypeDataFieldID]; }
        }
        protected void rgSummaryDisplayTypeGroups_SelectedIndexChanged(object sender, EventArgs e) {
            SetSelectedConfiguration();
        }
        void SetSelectedConfiguration() {
            SetSelectedGroup();
            SetSelectedSummaryDisplayType();
        }
        void SetSelectedGroup() {
            ConfigurePivotGridLayout(SelectedGroup);
            bool isVariation = (SelectedGroup == SummaryDisplayTypeGroup.Variation);
            cbAllowCrossGroupVariation.Visible = isVariation;
            if(SourceDataField != null)
                cbShowRawValues.Checked = SourceDataField.Visible;
            ConfigureSummaryDisplayTypeComboBox(SelectedGroup);
        }
        void ConfigurePivotGridLayout(SummaryDisplayTypeGroup typeGroup) {
            pivotGrid.BeginUpdate();
            switch(typeGroup) {
                case SummaryDisplayTypeGroup.Variation: {
                        pivotGrid.DataSourceID = "SalesPersonsDataSource";
                        pivotGrid.Fields.Clear();
                        PivotGridField fieldYear = pivotGrid.Fields.AddDataSourceColumn("OrderDate", PivotArea.ColumnArea, PivotGroupInterval.DateYear );
                        fieldYear.Caption = "Year";
                        PivotGridField fieldQuarter = pivotGrid.Fields.AddDataSourceColumn("OrderDate", PivotArea.ColumnArea, PivotGroupInterval.DateQuarter);
                        fieldQuarter.ValueFormat.FormatString = "Qtr {0}";
                        fieldQuarter.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        fieldQuarter.Caption = "Quarter";
                        PivotGridField salesPersonField = pivotGrid.Fields.AddDataSourceColumn("Sales Person", PivotArea.RowArea);
                        SourceDataFieldName = "OrderID";
                        PivotGridField sourceDataField = pivotGrid.Fields.AddDataSourceColumn(SourceDataFieldName, PivotArea.DataArea);
                        sourceDataField.SummaryType = PivotSummaryType.Count;
                        sourceDataField.Caption = "Order Count";
                        sourceDataField.ID = SourceDataFieldID;
                        PivotGridField summaryDisplayTypeDataField = pivotGrid.Fields.AddDataSourceColumn(SourceDataFieldName, PivotArea.DataArea); 
                        summaryDisplayTypeDataField.SummaryType = sourceDataField.SummaryType;
                        summaryDisplayTypeDataField.ID = SummaryDisplayTypeDataFieldID;
                    }
                    break;
                case SummaryDisplayTypeGroup.Percentage: {
                        pivotGrid.DataSourceID = "ProductReportsDataSource";
                        pivotGrid.Fields.Clear();
                        PivotGridField fieldYear = pivotGrid.Fields.AddDataSourceColumn("ShippedDate",PivotArea.ColumnArea, PivotGroupInterval.DateYear);
                        fieldYear.Caption = "Year";
                        PivotGridField fieldMonth = pivotGrid.Fields.AddDataSourceColumn("ShippedDate", PivotArea.ColumnArea, PivotGroupInterval.DateMonth);
                        fieldMonth.Caption = "Month";
                        PivotGridField categoryNameField = pivotGrid.Fields.AddDataSourceColumn("CategoryName", PivotArea.RowArea);
                        PivotGridField productNameField = pivotGrid.Fields.AddDataSourceColumn("ProductName", PivotArea.RowArea);
                        SourceDataFieldName = "ProductSales";
                        PivotGridField sourceDataField = pivotGrid.Fields.AddDataSourceColumn(SourceDataFieldName, PivotArea.DataArea);
                        sourceDataField.ID = SourceDataFieldID;
                        PivotGridField summaryDisplayTypeDataField = pivotGrid.Fields.AddDataSourceColumn(SourceDataFieldName, PivotArea.DataArea);
                        summaryDisplayTypeDataField.ID = SummaryDisplayTypeDataFieldID;
                    }
                    break;
                case SummaryDisplayTypeGroup.Rank: {
                        pivotGrid.DataSourceID = "SalesPersonsDataSource";
                        pivotGrid.Fields.Clear();
                        PivotGridField fieldYear = pivotGrid.Fields.AddDataSourceColumn("OrderDate", PivotArea.ColumnArea, PivotGroupInterval.DateYear);
                        fieldYear.Caption = "Year";
                        PivotGridField fieldQuarter = pivotGrid.Fields.AddDataSourceColumn("OrderDate", PivotArea.ColumnArea, PivotGroupInterval.DateQuarter);
                        fieldQuarter.ValueFormat.FormatString = "Qtr {0}";
                        fieldQuarter.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        fieldQuarter.Caption = "Quarter";
                        PivotGridField countryField = pivotGrid.Fields.AddDataSourceColumn("Country", PivotArea.RowArea);
                        PivotGridField salesPersonField = pivotGrid.Fields.AddDataSourceColumn("Sales Person", PivotArea.RowArea);
                        SourceDataFieldName = "Extended Price";
                        PivotGridField sourceDataField = pivotGrid.Fields.AddDataSourceColumn(SourceDataFieldName, PivotArea.DataArea);
                        sourceDataField.Caption = "Sales";
                        sourceDataField.ID = SourceDataFieldID;
                        PivotGridField summaryDisplayTypeDataField = pivotGrid.Fields.AddDataSourceColumn(SourceDataFieldName, PivotArea.DataArea);
                        summaryDisplayTypeDataField.ID = SummaryDisplayTypeDataFieldID;
                    }
                    break;

            }
            pivotGrid.EndUpdate();
            pivotGrid.DataBind();
        }
        void ConfigureSummaryDisplayTypeComboBox(SummaryDisplayTypeGroup typeGroup) {
            List<PivotSummaryDisplayType> types = new List<PivotSummaryDisplayType>();
            switch(typeGroup) {
                case SummaryDisplayTypeGroup.Variation:
                    types.Add(PivotSummaryDisplayType.AbsoluteVariation);
                    types.Add(PivotSummaryDisplayType.PercentVariation);
                    break;
                case SummaryDisplayTypeGroup.Percentage:
                    types.Add(PivotSummaryDisplayType.PercentOfColumn);
                    types.Add(PivotSummaryDisplayType.PercentOfRow);
                    types.Add(PivotSummaryDisplayType.PercentOfColumnGrandTotal);
                    types.Add(PivotSummaryDisplayType.PercentOfRowGrandTotal);
                    types.Add(PivotSummaryDisplayType.PercentOfGrandTotal);
                    break;
                case SummaryDisplayTypeGroup.Rank:
                    types.Add(PivotSummaryDisplayType.RankInColumnLargestToSmallest);
                    types.Add(PivotSummaryDisplayType.RankInColumnSmallestToLargest);
                    types.Add(PivotSummaryDisplayType.RankInRowLargestToSmallest);
                    types.Add(PivotSummaryDisplayType.RankInRowSmallestToLargest);
                    break;
            }
            ddlSummaryDisplayType.Items.Clear();
            foreach(PivotSummaryDisplayType type in types)
                ddlSummaryDisplayType.Items.Add(Enum.GetName(typeof(PivotSummaryDisplayType), type), type);
            ddlSummaryDisplayType.SelectedIndex = 0;
        }
        protected void cbShowRawValues_CheckedChanged(object sender, EventArgs e) {
            if(SourceDataFieldName != null) {
                SourceDataField.Visible = cbShowRawValues.Checked;
                if(SourceDataField.Visible)
                    SourceDataField.AreaIndex = 0;
            }
        }
        protected void cbAllowCrossGroupVariation_CheckedChanged(object sender, EventArgs e) {
            pivotGrid.OptionsData.AllowCrossGroupVariation = cbAllowCrossGroupVariation.Checked;
        }
        protected void ddlSummaryDisplayType_SelectedIndexChanged(object sender, EventArgs e) {
            SetSelectedSummaryDisplayType();
        }
        void SetSelectedSummaryDisplayType() {
            if(SummaryDisplayTypeDataField == null)
                return;
            DataSourceColumnBinding sourceBinding = new DataSourceColumnBinding(SourceDataFieldName);
            switch(ddlSummaryDisplayType.SelectedItem.Text) {
                case "AbsoluteVariation":
                    SummaryDisplayTypeDataField.DataBinding = new DifferenceBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.RowValue,
                        CalculationDirection.DownThenAcross,
                        DifferenceTarget.Previous,
                        DifferenceType.Absolute);
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "n0";
                    break;
                case "PercentVariation":
                    SummaryDisplayTypeDataField.DataBinding = new DifferenceBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.RowValue,
                        CalculationDirection.DownThenAcross,
                        DifferenceTarget.Previous,
                        DifferenceType.Percentage);
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "p2";
                    break;
                case "PercentOfColumn":
                    SummaryDisplayTypeDataField.DataBinding = new PercentOfTotalBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.ColumnValueAndRowParentValue);
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "p2";
                    break;
                case "PercentOfRow":
                    SummaryDisplayTypeDataField.DataBinding = new PercentOfTotalBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.RowValueAndColumnParentValue);
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "p2";
                    break;
                case "PercentOfColumnGrandTotal":
                    SummaryDisplayTypeDataField.DataBinding = new PercentOfTotalBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.ColumnValue);
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "p2";

                    break;
                case "PercentOfRowGrandTotal":
                    SummaryDisplayTypeDataField.DataBinding = new PercentOfTotalBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.RowValue);
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "p2";
                    break;
                case "PercentOfGrandTotal":
                    SummaryDisplayTypeDataField.DataBinding = new PercentOfTotalBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.None);
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "p2";
                    break;
                case "RankInColumnLargestToSmallest":
                    SummaryDisplayTypeDataField.DataBinding = new RankBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.ColumnValue,
                        RankType.Dense, PivotSortOrder.Descending);
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "n0";
                    break;
                case "RankInColumnSmallestToLargest":
                    SummaryDisplayTypeDataField.DataBinding = new RankBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.ColumnValue,
                        RankType.Dense, PivotSortOrder.Ascending);
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "n0";
                    break;
                case "RankInRowLargestToSmallest":
                    SummaryDisplayTypeDataField.DataBinding = new RankBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.RowValue,
                        RankType.Dense, PivotSortOrder.Descending);
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "n0";
                    break;
                case "RankInRowSmallestToLargest":
                    SummaryDisplayTypeDataField.DataBinding = new RankBinding(
                        sourceBinding,
                        CalculationPartitioningCriteria.ColumnValue,
                        RankType.Dense, PivotSortOrder.Ascending);
                    SummaryDisplayTypeDataField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    SummaryDisplayTypeDataField.CellFormat.FormatString = "n0";
                    break;
            }
            SummaryDisplayTypeDataField.Caption = string.Format("{0}", ddlSummaryDisplayType.SelectedItem.Text);
        }
    }
}
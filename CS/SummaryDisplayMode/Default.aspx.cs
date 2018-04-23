using System;
using System.Collections.Generic;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.Data.PivotGrid;

namespace SummaryDisplayMode {
    public partial class DefaultForm : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack && !IsCallback)
                SetSelectedConfiguration();
        }
        const string SummaryDisplayTypeDataFieldID = "summaryDisplayTypeDataField";

        enum SummaryDisplayTypeGroup { Variation = 0, Percentage = 1, Rank = 2, Index = 3 };

        SummaryDisplayTypeGroup SelectedGroup {
            get { return (SummaryDisplayTypeGroup)(rgSummaryDisplayTypeGroups.SelectedIndex); }
        }
        string SourceDataFieldName {
            get { return cachedSourceDataFieldName.Value; }
            set { cachedSourceDataFieldName.Value = value; }
        }
        PivotGridField SourceDataField {
            get { return pivotGrid.Fields[SourceDataFieldName]; }
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
            cbHideEmptyVariationItems.Visible = isVariation;
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
                        PivotGridField fieldYear = pivotGrid.Fields.Add("OrderDate", DevExpress.XtraPivotGrid.PivotArea.ColumnArea);
                        fieldYear.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear;
                        fieldYear.Caption = "Year";
                        PivotGridField fieldQuarter = pivotGrid.Fields.Add("OrderDate", DevExpress.XtraPivotGrid.PivotArea.ColumnArea);
                        fieldQuarter.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateQuarter;
                        fieldQuarter.ValueFormat.FormatString = "Qtr {0}";
                        fieldQuarter.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        fieldQuarter.Caption = "Quarter";
                        pivotGrid.Fields.Add("Sales Person", DevExpress.XtraPivotGrid.PivotArea.RowArea);
                        PivotGridField sourceDataField = pivotGrid.Fields.Add("OrderID", DevExpress.XtraPivotGrid.PivotArea.DataArea);
                        sourceDataField.SummaryType = PivotSummaryType.Count;
                        sourceDataField.Caption = "Order Count";
                        SourceDataFieldName = sourceDataField.FieldName;
                        PivotGridField summaryDisplayTypeDataField = pivotGrid.Fields.Add(SourceDataFieldName, DevExpress.XtraPivotGrid.PivotArea.DataArea);
                        summaryDisplayTypeDataField.SummaryType = sourceDataField.SummaryType;
                        summaryDisplayTypeDataField.ID = SummaryDisplayTypeDataFieldID;
                    }
                    break;
                case SummaryDisplayTypeGroup.Percentage: {
                        pivotGrid.DataSourceID = "ProductReportsDataSource";
                        pivotGrid.Fields.Clear();
                        PivotGridField fieldYear = pivotGrid.Fields.Add("ShippedDate", DevExpress.XtraPivotGrid.PivotArea.ColumnArea);
                        fieldYear.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear;
                        fieldYear.Caption = "Year";
                        PivotGridField fieldMonth = pivotGrid.Fields.Add("ShippedDate", DevExpress.XtraPivotGrid.PivotArea.ColumnArea);
                        fieldMonth.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth;
                        fieldMonth.Caption = "Month";
                        pivotGrid.Fields.Add("CategoryName", DevExpress.XtraPivotGrid.PivotArea.RowArea);
                        pivotGrid.Fields.Add("ProductName", DevExpress.XtraPivotGrid.PivotArea.RowArea);
                        PivotGridField sourceDataField = pivotGrid.Fields.Add("ProductSales", DevExpress.XtraPivotGrid.PivotArea.DataArea);
                        SourceDataFieldName = sourceDataField.FieldName;
                        PivotGridField summaryDisplayTypeDataField = pivotGrid.Fields.Add(SourceDataFieldName, DevExpress.XtraPivotGrid.PivotArea.DataArea);
                        summaryDisplayTypeDataField.ID = SummaryDisplayTypeDataFieldID;
                    }
                    break;
                case SummaryDisplayTypeGroup.Rank: {
                        pivotGrid.DataSourceID = "SalesPersonsDataSource";
                        pivotGrid.Fields.Clear();
                        PivotGridField fieldYear = pivotGrid.Fields.Add("OrderDate", DevExpress.XtraPivotGrid.PivotArea.ColumnArea);
                        fieldYear.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear;
                        fieldYear.Caption = "Year";
                        PivotGridField fieldQuarter = pivotGrid.Fields.Add("OrderDate", DevExpress.XtraPivotGrid.PivotArea.ColumnArea);
                        fieldQuarter.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateQuarter;
                        fieldQuarter.ValueFormat.FormatString = "Qtr {0}";
                        fieldQuarter.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        fieldQuarter.Caption = "Quarter";
                        pivotGrid.Fields.Add("Country", DevExpress.XtraPivotGrid.PivotArea.RowArea);
                        pivotGrid.Fields.Add("Sales Person", DevExpress.XtraPivotGrid.PivotArea.RowArea);
                        PivotGridField sourceDataField = pivotGrid.Fields.Add("Extended_Price", DevExpress.XtraPivotGrid.PivotArea.DataArea);
                        sourceDataField.Caption = "Sales";
                        SourceDataFieldName = sourceDataField.FieldName;
                        PivotGridField summaryDisplayTypeDataField = pivotGrid.Fields.Add(SourceDataFieldName, DevExpress.XtraPivotGrid.PivotArea.DataArea);
                        summaryDisplayTypeDataField.ID = SummaryDisplayTypeDataFieldID;
                    }
                    break;
                case SummaryDisplayTypeGroup.Index: {
                        pivotGrid.DataSourceID = "SalesPersonsDataSource";
                        pivotGrid.Fields.Clear();
                        PivotGridField fieldYear = pivotGrid.Fields.Add("Country", DevExpress.XtraPivotGrid.PivotArea.ColumnArea);
                        pivotGrid.Fields.Add("CategoryName", DevExpress.XtraPivotGrid.PivotArea.RowArea);
                        PivotGridField sourceDataField = pivotGrid.Fields.Add("Extended_Price", DevExpress.XtraPivotGrid.PivotArea.DataArea);
                        sourceDataField.Caption = "Sales";
                        SourceDataFieldName = sourceDataField.FieldName;
                        PivotGridField summaryDisplayTypeDataField = pivotGrid.Fields.Add(SourceDataFieldName, DevExpress.XtraPivotGrid.PivotArea.DataArea);
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
                case SummaryDisplayTypeGroup.Index:
                    types.Add(PivotSummaryDisplayType.Index);
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
        protected void cbHideEmptyVariationItems_CheckedChanged(object sender, EventArgs e) {
            if(SummaryDisplayTypeDataField != null)
                SummaryDisplayTypeDataField.Options.HideEmptyVariationItems = cbHideEmptyVariationItems.Checked;
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
            SummaryDisplayTypeDataField.SummaryDisplayType = (PivotSummaryDisplayType)Enum.Parse(typeof(PivotSummaryDisplayType), ddlSummaryDisplayType.SelectedItem.Text);
            SummaryDisplayTypeDataField.Caption = string.Format("{0}", SummaryDisplayTypeDataField.SummaryDisplayType);
        }

    }
}
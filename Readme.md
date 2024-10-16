<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128577015/22.1.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T590012)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# Pivot Grid for Web Forms - How to Change the Summary Display Mode

This example shows how to use different summary display modes.

![Pivot Grid for Web Forms - Calculate Running Totals](images/pivot-grid-web-forms-change-summary-mode.png)

Уou can display differences between summaries in the current and previous cells, or the percentage of a column's or row's total. Use the **Summary Display Type** combo box to choose the summary display mode. Use the [Data Binding API](https://docs.devexpress.com/CoreLibraries/401533/devexpress-pivot-grid-core-library/data-binding-api?v=22.1) to specify how summary values should be displayed within cells.

The following classes are used in this example:

- [DifferenceBinding](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxPivotGrid.DifferenceBinding)
- [PercentOfTotalBinding](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxPivotGrid.PercentOfTotalBinding)
- [RankBinding](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxPivotGrid.RankBinding)

## Files to Look At

* [Default.aspx](./CS/SummaryDisplayMode/Default.aspx) (VB: [Default.aspx](./VB/SummaryDisplayMode/Default.aspx))
* [Default.aspx.cs](./CS/SummaryDisplayMode/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/SummaryDisplayMode/Default.aspx.vb))

## Documentation

- [Summary Display Modes](https://docs.devexpress.com/AspNet/7281/components/pivot-grid/data-shaping/aggregation/summaries/summary-display-modes)
- [Window Calculations Overview](https://docs.devexpress.com/CoreLibraries/401364/devexpress-pivot-grid-core-library/advanced-analytics/window-calculations/window-calculations-overview)




<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=web-forms-pivot-grid-change-summary-display-mode&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=web-forms-pivot-grid-change-summary-display-mode&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->

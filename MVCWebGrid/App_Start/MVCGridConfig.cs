[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MVCWebGrid.MVCGridConfig), "RegisterGrids")]

namespace MVCWebGrid
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;

    using MVCGrid.Models;
    using MVCGrid.Web;
    using Models;

    public static class MVCGridConfig
    {
        public static void RegisterGrids()
        {


            MVCGridDefinitionTable.Add("SampleGrid", new MVCGridBuilder<Customer>()
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    // Add your columns here
                    cols.Add().WithColumnName("CustomerId").WithHeaderText("Customer ID")
                              .WithValueExpression(i => i.CustomerId.ToString()); // use the Value Expression to return the cell text for this column
                    cols.Add().WithColumnName("FirstName").WithHeaderText("First Name").WithValueExpression(i => i.FirstName);
                    cols.Add().WithColumnName("LastName").WithHeaderText("Last Name").WithValueExpression(i => i.LastName);
                    cols.Add().WithColumnName("FullaName").WithHeaderText("Full Name").WithValueTemplate("{Model.FirstName} {Model.LastName}")
                              .WithVisibility(visible: false, allowChangeVisibility: true).WithSorting(true);
                    cols.Add().WithColumnName("OrderDate").WithHeaderText("Order Date")
                              .WithValueExpression(i =>i.OrderDate.ToString());
                    cols.Add().WithColumnName("Status").WithHeaderText("Status").WithValueExpression(i=>i.Status);
                })
                .WithRetrieveDataMethod((context) =>
                {
                    // Query your data here. Obey Ordering, paging and filtering parameters given in the context.QueryOptions.
                    // Use Entity Framework, a module from your IoC Container, or any other method.
                    // Return QueryResult object containing IEnumerable<YouModelItem>

                    var lists = new QueryResult<Customer>();

                    using (MyDbContext db = new MyDbContext ())
                    {
                        lists.Items = db.Customers.ToList();
                    }

                    return lists;

                })
            );

        }
    }
}
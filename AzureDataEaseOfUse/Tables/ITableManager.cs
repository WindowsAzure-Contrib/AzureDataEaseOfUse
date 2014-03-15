﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureDataEaseOfUse.Tables;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureDataEaseOfUse
{
    public interface ITableManager<T> where T : IAzureDataTableEntity
    {

        string GetTableName();
        
        IConnectionManager ConnectionManager { get; }


        Task<TableOperationResult> Execute(TableOperation operation);

        IList<Task<TableOperationResult>> Execute(params TableOperation[] operations);

        IList<Task<TableOperationResult>> Execute(IEnumerable<TableOperation> operations);


        Task<TableBatchResult> Execute(TableBatchOperation batch);

        IList<Task<TableBatchResult>> Execute(params TableBatchOperation[] batches);

        IList<Task<TableBatchResult>> Execute(IEnumerable<TableBatchOperation> batches);

    }

}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

namespace AzureDataEaseOfUse
{
    public static class ConnectionExtensions
    {



        public static string GetConnectionString(string connectionStringOrName)
        {
            var fromSettings = ConfigurationManager.ConnectionStrings[connectionStringOrName].ConnectionString;

            return string.IsNullOrEmpty(fromSettings) ? fromSettings : connectionStringOrName;
        }

        #region Setup Best Practices (Per AzureWeek)

        /// <summary>
        /// Disabled Nagle. Disables Expect100Continue. Increases DefaultConnectionLimit to 1000.
        /// Executes automatically on app start
        /// </summary>
        public static void SetupBestPractices()
        {
            NagleAlgorithm(false);
            Expect100Continue(false);
            DefaultConnectionLimit(1000);
        }

        public static void SetupBestPracticies(this CloudStorageAccount account)
        {
            account.NagleAlgorithm(false);
            account.Expect100Continue(false);
        }

        #endregion

        #region Setting Overrides

        /// <summary>
        /// Overrides the Best Practice (Disable for small messages, less than 1400 b) already set for this connection
        /// </summary>
        public static CloudStorageAccount NagleAlgorithm(this CloudStorageAccount account, bool enabled)
        {
            account.TableServicePoint().UseNagleAlgorithm = enabled;

            return account;
        }

        /// <summary>
        /// [Global] Overrides the Best Practice (Disable for small messages, less than 1400 b) already set for this connection
        /// </summary>
        public static void NagleAlgorithm(bool enabled)
        {
            ServicePointManager.UseNagleAlgorithm = enabled;
        }


        /// <summary>
        /// Overrides the Best Practice (aka Disabled) already set for this connection
        /// </summary>
        public static CloudStorageAccount Expect100Continue(this CloudStorageAccount account, bool enabled)
        {
            account.TableServicePoint().Expect100Continue = enabled;

            return account;
        }

        /// <summary>
        /// [Global] Overrides the Best Practice (aka Disabled) already set for this connection
        /// </summary>
        public static void Expect100Continue(bool enabled)
        {
            ServicePointManager.Expect100Continue = enabled;
        }

        /// <summary>
        /// [Global] Overrides the Best Practice (aka 100+).
        /// </summary>
        public static void DefaultConnectionLimit(int limit)
        {
            ServicePointManager.DefaultConnectionLimit = limit;
        }

        #endregion

        #region Infrastructure (ServicePoint, Table Keys)

        public static ServicePoint TableServicePoint(this CloudStorageAccount account)
        {
            return ServicePointManager.FindServicePoint(account.TableEndpoint);
        }


        #endregion


    }
}
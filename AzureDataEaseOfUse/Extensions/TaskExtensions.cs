﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataEaseOfUse
{
    public static class TaskExtensions
    {

        public static Task OnCompletion<TResult>(this Task<TResult> task, Action<Task<TResult>> continuationAction)
        {
            return task.ContinueWith(continuationAction, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public static Task OnCancelled<TResult>(this Task<TResult> task, Action<Task<TResult>> continuationAction)
        {
            return task.ContinueWith(continuationAction, TaskContinuationOptions.OnlyOnCanceled);
        }

        public static Task OnFaulted<TResult>(this Task<TResult> task, Action<Task<TResult>> continuationAction)
        {
            return task.ContinueWith(continuationAction, TaskContinuationOptions.OnlyOnFaulted);
        }

    }
}

/////////////////////////////////////////////////////////////////////
// Copyright (c) Autodesk, Inc. All rights reserved
// Written by Autodesk Design Automation team for Inventor
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
/////////////////////////////////////////////////////////////////////

using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApplication.Definitions;
using WebApplication.Processing;

namespace WebApplication.Job
{
    public abstract class JobItemBase
    {
        protected ILogger Logger { get; }
        protected ProjectWork ProjectWork { get; }
        public string ProjectId { get; }
        public string Id { get; }

        protected JobItemBase(ILogger logger, string projectId, ProjectWork projectWork)
        {
            ProjectId = projectId;
            Id = Guid.NewGuid().ToString();
            ProjectWork = projectWork;
            Logger = logger;
        }

        public abstract Task ProcessJobAsync(IResultSender resultSender);

        public override string GetMessage()
        {
            return message;
        }

        public override async Task ProcessJobAsync(IResultSender resultSender, string message)
        {
            using System.IDisposable scope = Logger.BeginScope("Update Model ({Id})");

            Logger.LogInformation($"ProcessJob (Update) {Id} for project {ProjectId} started.");

            (ProjectStateDTO state, FdaStatsDTO stats, string reportUrl) = await ProjectWork.DoSmartUpdateAsync(Parameters, ProjectId);

            string message = $"ProcessJob (Update) {Id} for project {ProjectId} completed.";
            Logger.LogInformation(message: message);

            // send that we are done to client
            await resultSender.SendSuccessAsync(state, stats, reportUrl);
        }
    }
}

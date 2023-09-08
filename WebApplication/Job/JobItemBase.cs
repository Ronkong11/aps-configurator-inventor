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

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Shared;
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
        public InventorParameters Parameters { get; private set; }

        protected JobItemBase(ILogger logger, string projectId, ProjectWork projectWork)
        {
            ProjectId = projectId;
            Id = Guid.NewGuid().ToString();
            ProjectWork = projectWork;
            Logger = logger;
        }

        public abstract Task ProcessJobAsync(IResultSender resultSender, object v);
        internal abstract object Message();
        internal abstract Task ProcessJobAsync(Sender sender, object v);

        public override async Task ProcessJobAsync(IResultSender resultSender)
        {
            const string State = "RFA generation ({Id})";
            using System.IDisposable scope = Logger.BeginScope(State);

            Logger.LogInformation($"ProcessJob (RFA) {Id} for project {ProjectId} started.");

            (var stats, var reportUrl) = await ProjectWork.GenerateRfaAsync(ProjectId, _hash);
            Logger.LogInformation($"ProcessJob (RFA) {Id} for project {ProjectId} completed.");

            // TODO: this url can be generated right away... we can simply acknowledge that the OSS file is ready,
            // without generating a URL here
            string rfaUrl = _linkGenerator.GetPathByAction(controller: "Download",
                                                            action: "RFA",
                                                            values: new {projectName = ProjectId, hash = _hash});

            // send resulting URL to the client
            await resultSender.SendSuccessAsync(rfaUrl, stats, reportUrl);
        }
    }
}

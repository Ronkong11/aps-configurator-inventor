﻿/////////////////////////////////////////////////////////////////////
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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using WebApplication.Definitions;
using WebApplication.Processing;

namespace WebApplication.Job
{
    internal class DrawingJobItem(ILogger logger, string projectId, string hash, ProjectWork projectWork, LinkGenerator linkGenerator) : JobItemBase(logger, projectId, projectWork)
    {
        private readonly string _hash = hash;
        private readonly LinkGenerator _linkGenerator = linkGenerator;

        public override async Task ProcessJobAsync(IResultSender resultSender)
        {
            using System.IDisposable scope = Logger.BeginScope("Drawing generation ({Id})");

            string message = $"ProcessJob (Drawing) {Id} for project {ProjectId} started.";
            Logger.LogInformation(message: message);

            (FdaStatsDTO stats, string reportUrl) = await ProjectWork.GenerateDrawingAsync(ProjectId, _hash);
            Logger.LogInformation($"ProcessJob (Drawing) {Id} for project {ProjectId} completed.");

            // TODO: this url can be generated right away... we can simply acknowledge that OSS file is ready,
            // without generating URL here
            var drawingUrl = _linkGenerator.GetPathByAction(controller: "Download",
                                                            action: "Drawing",
                                                            values: new { projectName = ProjectId, hash = _hash });

            // send resulting URL to the client
            await resultSender.SendSuccessAsync(drawingUrl, stats, reportUrl);
        }

        public override Task ProcessJobAsync(IResultSender resultSender, object v)
        {
            throw new System.NotImplementedException();
        }

        internal override object Message()
        {
            throw new System.NotImplementedException();
        }

        internal override Task ProcessJobAsync(Sender sender, object v)
        {
            throw new System.NotImplementedException();
        }
    }
}
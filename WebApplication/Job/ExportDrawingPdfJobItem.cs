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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using WebApplication.Definitions;
using WebApplication.Processing;

namespace WebApplication.Job
{
    /// <summary>
    /// Generate drawing PDF.
    /// </summary>
    internal class ExportDrawingPdfJobItem(ILogger logger, string projectId, string hash, string drawingKey, ProjectWork projectWork, LinkGenerator linkGenerator) : JobItemBase(logger, projectId, projectWork)
    {
        private readonly string _hash = hash;
        private readonly LinkGenerator _linkGenerator = linkGenerator;
        private readonly string _drawingKey = drawingKey;

        public override async Task ProcessJobAsync(IResultSender resultSender)
        {
            const string State = "Export Drawing PDF ({Id})";
            using System.IDisposable scope = Logger.BeginScope(state: State);
            Logger.LogInformation($"ProcessJob (ExportDrawingPDF) {Id} for project {ProjectId} started.");

            (FdaStatsDTO stats, int drawingIndex, string reportUrl) = await ProjectWork.ExportDrawingPdfAsync(
                ProjectId,
                _hash,
                drawingKey: _drawingKey);

            string message = $"ProcessJob (ExportDrawingPDF) {Id} for project {ProjectId} completed.";
            Logger.LogInformation(message);

            string url = "";
            if (stats != null)
            {
                url = _linkGenerator.GetPathByAction(controller: "Download",
                                                                action: "DrawingPdf",
                                                                values: new { projectName = ProjectId, hash = _hash, index = drawingIndex });

                // when local url starts with a slash, it does not work, because it is doubled in url
                if (url.StartsWith('/'))
                {
                    url = url[1..];
                }
            }

            await resultSender.SendSuccessAsync(url, stats, reportUrl);
        }
    }
}
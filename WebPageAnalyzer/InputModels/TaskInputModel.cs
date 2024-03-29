﻿using System.ComponentModel.DataAnnotations;
using WebPageAnalyzer.Validations;

namespace WebPageAnalyzer.Storage.Dto;

public class TaskInputModel
{
    [Required]
    [StringLength(int.MaxValue, MinimumLength = 3, ErrorMessage = "Minimum length 3 chars")]
    [UrlValidation]
    public string Url { get; set; }

    [Required] public List<string> Words { get; set; }

    [Required] [CronValidation] public string CronExpression { get; set; }
}
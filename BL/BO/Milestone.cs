﻿
namespace BO;

public class Milestone
{
    public int ID { get; init; }
    public required string description { get; set; }
    public required string alias { get; set; }
    public Status? status { get; set; }
    public DateTime createdAt { get; init; }
    public DateTime? start { get; set; }
    public DateTime? forecastDate { get; set; }
    public DateTime deadline { get; init; }
    public DateTime? complete { get; set; }
    public int? completionPercentage { get; set; }
    public string? remarks { get; set; }
    public TaskInList? dependencies { get; set; }
}
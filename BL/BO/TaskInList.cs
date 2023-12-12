﻿
namespace BO;

public class TaskInList
{
    public int ID { get; init; }
    public required string description { get; set; }
    public required string alias { get; set; }
    public Status? status { get; set; }
}
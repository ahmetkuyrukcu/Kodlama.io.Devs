﻿namespace Core.ElasticSearch.Models;

public class SearchParameters
{
    public string IndexName { get; set; }

    public int From { get; set; }

    public int Size { get; set; } = 10;
}
﻿namespace FoodCorner.Services.Abstracts
{
    public interface IOrderService
    { 
        Task<string> GenerateUniqueTrackingCodeAsync();
    }
}

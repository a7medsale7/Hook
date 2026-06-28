using System;

namespace Hook.Application.Contracts.Admin;

public record AdminDashboardStatsResponse(
    int TotalUsers,
    int TotalTripManagers,
    int TotalSellers,
    int TotalTrips,
    int TotalProducts,
    int TotalOrders,
    decimal TotalRevenue,
    int TotalBookings
);

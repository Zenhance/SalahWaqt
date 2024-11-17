namespace SalahWaqt.Models;

public record AladhanResponse(
    int Code,
    string Status,
    Data Data
);

public record Data(
    Timings Timings,
    DateInfo Date,
    Meta Meta
);

public record Timings(
    string Fajr,
    string Sunrise,
    string Dhuhr,
    string Asr,
    string Sunset,
    string Maghrib,
    string Isha,
    string Imsak,
    string Midnight,
    string Firstthird,
    string Lastthird
);

public record DateInfo(
    string Readable,
    string Timestamp,
    Hijri Hijri,
    Gregorian Gregorian
);

public record Hijri(
    string Date,
    string Format,
    string Day,
    Weekday Weekday,
    Month Month,
    string Year,
    Designation Designation,
    List<string> Holidays
);

public record Gregorian(
    string Date,
    string Format,
    string Day,
    Weekday Weekday,
    Month Month,
    string Year,
    Designation Designation
);

public record Weekday(
    string En,
    string Ar
);

public record Month(
    int Number,
    string En,
    string Ar
);

public record Designation(
    string Abbreviated,
    string Expanded
);

public record Meta(
    double Latitude,
    double Longitude,
    string Timezone,
    Method Method,
    string LatitudeAdjustmentMethod,
    string MidnightMode,
    string School,
    Offset Offset
);

public record Method(
    int Id,
    string Name,
    MethodParams Params,
    Location Location
);

public record MethodParams(
    int Fajr,
    int Isha
);

public record Location(
    double Latitude,
    double Longitude
);

public record Offset(
    int Imsak,
    int Fajr,
    int Sunrise,
    int Dhuhr,
    int Asr,
    int Maghrib,
    int Sunset,
    int Isha,
    int Midnight
);

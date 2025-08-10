using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NpgsqlTypes;

namespace Core.Persistence.EntityFrameworkCore.Npgsql.Extensions
{
    public static class NpgsqlDbTypeExtensions
    {
        public static string ToPostgresType(this NpgsqlDbType type)
        {
            return type switch
            {
                // JSON
                NpgsqlDbType.Jsonb => "jsonb",
                NpgsqlDbType.Json => "json",

                // UUID
                NpgsqlDbType.Uuid => "uuid",

                // Metin
                NpgsqlDbType.Text => "text",
                NpgsqlDbType.Varchar => "varchar",
                NpgsqlDbType.Char => "char",
                NpgsqlDbType.Name => "name", // PostgreSQL özel

                // Sayısal
                NpgsqlDbType.Integer => "integer",
                NpgsqlDbType.Bigint => "bigint",
                NpgsqlDbType.Smallint => "smallint",
                NpgsqlDbType.Numeric => "numeric",
                NpgsqlDbType.Real => "real",
                NpgsqlDbType.Double => "double precision",
                NpgsqlDbType.Money => "money",

                // Boolean
                NpgsqlDbType.Boolean => "boolean",

                // Tarih & Saat
                NpgsqlDbType.Timestamp => "timestamp without time zone",
                NpgsqlDbType.TimestampTz => "timestamp with time zone",
                NpgsqlDbType.Date => "date",
                NpgsqlDbType.Time => "time without time zone",
                NpgsqlDbType.TimeTz => "time with time zone",
                NpgsqlDbType.Interval => "interval",

                // Binary
                NpgsqlDbType.Bytea => "bytea",

                // Array
                NpgsqlDbType.Array | NpgsqlDbType.Text => "text[]",
                NpgsqlDbType.Array | NpgsqlDbType.Integer => "integer[]",
                NpgsqlDbType.Array | NpgsqlDbType.Uuid => "uuid[]",

                // Varsayılan
                _ => type.ToString().ToLower()
            };
        }
    }
}

using System.Diagnostics.CodeAnalysis;

namespace ChordAnalyzer
{
    public struct Position
    {
        public decimal Row { get; set; }
        public decimal Column { get; set; }

        public Position(decimal row, decimal column)
        {
            Row = row;
            Column = column;
        }

        public decimal Distance(Position p){
            decimal cr = Row - p.Row;
            decimal cc = Column - p.Column;
            return (decimal)Math.Sqrt((double)(cr * cr + cc * cc));
        }

        public static Position operator +(Position a, Position b) => new Position(a.Row + b.Row, a.Column + b.Column);
        public static Position operator /(Position a, decimal b) => new Position(a.Row / b, a.Column / b);
        public static bool operator ==(Position a, Position b) => a.Row == b.Row && a.Column == b.Column;
        public static bool operator !=(Position a, Position b) => a.Row != b.Row || a.Column != b.Column;

        public override bool Equals([NotNullWhen(true)] object? obj) => this == obj as Position?;
        public override int GetHashCode() => HashCode.Combine(Row, Column);
    }
}
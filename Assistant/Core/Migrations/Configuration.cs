namespace Core.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Core.DB.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Core.DB.DataContext context)
        {
            //context.MeetingStatuses.AddRange(new List<MeetingStatus>
            //{
            //  new MeetingStatus
            //  {
            //      Caption = "Met",
            //      Code = 1
            //  },
            //  new MeetingStatus
            //  {
            //      Caption = "Not Met",
            //      Code = 2
            //  }
            //});
        }
    }
}

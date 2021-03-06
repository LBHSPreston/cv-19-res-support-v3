using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Infrastructure;
using LBHFSSPublicAPI.Tests.TestHelpers;
using AutoFixture;

namespace cv19ResSupportV3.Tests.V4.TestHelpers
{
    public static class EntityHelpers
    {
        public static List<ResidentEntity> CreateResidentEntities(int count = 3)
        {
            var residentEntities = new List<ResidentEntity>();
            for (var a = 0; a < count; a++)
            {
                var residentEntity = Randomm.Build<ResidentEntity>()
                    .Without(h => h.RecordStatus)
                    .Without(h => h.CaseNotes)
                    .Without(h => h.HelpRequests)
                    .Create();
                residentEntities.Add(residentEntity);
            }
            return residentEntities;
        }

        public static HelpRequestEntity CreateHelpRequestEntity(int id = 1, int residentId = 1)
        {
            var helpRequestEntity = Randomm.Build<HelpRequestEntity>()
                .With(x => x.Id, id)
                .With(x => x.ResidentId, residentId)
                .Without(h => h.HelpRequestCalls)
                .Without(h => h.CaseNotes)
                .Without(h => h.ResidentEntity)
                .Create();
            return helpRequestEntity;
        }

        public static List<HelpRequestEntity> createHelpRequestEntities(int count = 3, int residentId = 1)
        {
            var helpRequestEntities = Randomm.Build<HelpRequestEntity>()
                .Without(h => h.HelpRequestCalls)
                .Without(h => h.CaseNotes)
                .Without(h => h.ResidentEntity)
                .With(x => x.ResidentId, residentId)
                .CreateMany(count)
                .ToList();
            return helpRequestEntities;
        }

        public static HelpRequestCallEntity createHelpRequestCallEntity()
        {
            return Randomm.Build<HelpRequestCallEntity>()
                .Without(h => h.HelpRequestEntity)
                .Create();
        }

        public static List<HelpRequestCallEntity> createHelpRequestCallEntities(int count = 3)
        {
            var calls = Randomm.Build<HelpRequestCallEntity>()
                .Without(h => h.HelpRequestEntity).CreateMany(count).ToList();
            return calls;
        }
    }
}

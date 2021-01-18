using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Amazon.Lambda.Core;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Domain.Queries;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Infrastructure;
using Microsoft.EntityFrameworkCore;
using HelpRequest = cv19ResSupportV3.V3.Domain.HelpRequest;

namespace cv19ResSupportV3.V3.Gateways
{
    public class HelpRequestGateway : IHelpRequestGateway
    {
        private readonly HelpRequestsContext _helpRequestsContext;

        public HelpRequestGateway(HelpRequestsContext helpRequestsContext)
        {
            _helpRequestsContext = helpRequestsContext;
        }

        public int CreateHelpRequest(int residentId, CreateHelpRequest command)
        {
            var requestEntity = command.ToEntity();
            if (requestEntity == null) return 0;

            requestEntity.ResidentId = residentId;

            // SetRecordStatus(requestEntity);
            requestEntity.CallbackRequired ??= true;
            try
            {
                _helpRequestsContext.HelpRequestEntities.Add(requestEntity);
                _helpRequestsContext.SaveChanges();
                return requestEntity.Id;
            }
            catch (Exception e)
            {
                LambdaLogger.Log("CreateHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public Resident CreateResident(CreateResident command)
        {
            var requestEntity = command.ToResidentEntity();
            if (requestEntity == null) return null;

            try
            {
                _helpRequestsContext.ResidentEntities.Add(requestEntity);
                _helpRequestsContext.SaveChanges();
                var resident = _helpRequestsContext.ResidentEntities.Find(requestEntity.Id);
                return resident.ToResidentDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("CreateHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public Resident UpdateResident(int residentId, UpdateResident command)
        {
            try
            {
                var updatedResidentEntity = _helpRequestsContext.ResidentEntities.Find(residentId);
                return updatedResidentEntity.ToResidentDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("UpdateResident error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }


        public int? FindResident(FindResident command)
        {
            try
            {
                if (command.Uprn != null)
                {
                    var response = _helpRequestsContext.ResidentEntities.FirstOrDefault(x => x.Uprn == command.Uprn &&
                                                                                             x.FirstName ==
                                                                                             command.FirstName &&
                                                                                             x.LastName ==
                                                                                             command.LastName);
                    if (response != null)
                    {
                        return response.Id;
                    }
                }

                if (command.DobDay != null || command.DobMonth != null || command.DobYear != null)
                {
                    var response = _helpRequestsContext.ResidentEntities.FirstOrDefault(x =>
                        x.DobDay == command.DobDay &&
                        x.DobMonth == command.DobMonth &&
                        x.DobYear == command.DobYear &&
                        x.FirstName == command.FirstName &&
                        x.LastName == command.LastName);
                    if (response != null)
                    {
                        return response.Id;
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                LambdaLogger.Log("FindResident error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public List<LookupDomain> GetLookups(LookupQuery command)
        {
            Expression<Func<LookupEntity, bool>> queryLookups = x =>
                string.IsNullOrWhiteSpace(command.LookupGroup)
                || x.LookupGroup.Replace(" ", "").ToUpper().Equals(command.LookupGroup.Replace(" ", "").ToUpper());
            try
            {
                var response = _helpRequestsContext.Lookups
                    .Where(queryLookups)
                    .OrderBy(x => x.LookupGroup)
                    .ThenBy(x => x.Lookup)
                    .ToList();
                return response.ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetLookups error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public HelpRequest UpdateHelpRequest(UpdateResidentAndHelpRequest command)
        {
            if (command == null) return null;
            try
            {
                //                var entity = _helpRequestsContext.HelpRequestEntities.Find(command.Id);
                //                _helpRequestsContext.Entry(entity).CurrentValues.SetValues(command);
                //                _helpRequestsContext.SaveChanges();
                //                return entity.ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("UpdateHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }

            return new HelpRequest();
        }

        public Resident GetResident(int id)
        {
            try
            {
                var resident = _helpRequestsContext.ResidentEntities
                    .Include(x => x.CaseNotes)
                    .FirstOrDefault(x => x.Id == id);
                return resident?.ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetResident error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public HelpRequestWithResident GetHelpRequest(int id)
        {
            try
            {
                var helpRequest = _helpRequestsContext.HelpRequestEntities
                    .Include(x => x.HelpRequestCalls)
                    .Include(x => x.ResidentEntity)
                    .Include(x => x.CaseNotes)
                    .FirstOrDefault(x => x.Id == id);
                return helpRequest?.ToHelpRequestWithResidentDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetResidentAndHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public List<HelpRequestWithResident> SearchHelpRequests(SearchRequest command)
        {
            Expression<Func<HelpRequestEntity, bool>> queryPostCode = x =>
                string.IsNullOrWhiteSpace(command.Postcode)
                || x.ResidentEntity.PostCode.Replace(" ", "").ToUpper()
                    .Contains(command.Postcode.Replace(" ", "").ToUpper());

            Expression<Func<HelpRequestEntity, bool>> queryFirstName = x =>
                string.IsNullOrWhiteSpace(command.FirstName)
                || x.ResidentEntity.FirstName.Replace(" ", "").ToUpper()
                    .Contains(command.FirstName.Replace(" ", "").ToUpper());

            Expression<Func<HelpRequestEntity, bool>> queryLastName = x =>
                string.IsNullOrWhiteSpace(command.LastName)
                || x.ResidentEntity.LastName.Replace(" ", "").ToUpper()
                    .Contains(command.LastName.Replace(" ", "").ToUpper());

            Expression<Func<HelpRequestEntity, bool>> queryHelpNeeded = x =>
                string.IsNullOrWhiteSpace(command.HelpNeeded)
                || x.HelpNeeded.Replace(" ", "").ToUpper().Equals(command.HelpNeeded.Replace(" ", "").ToUpper());


            try
            {
                return _helpRequestsContext.HelpRequestEntities
                    .Include(x => x.HelpRequestCalls)
                    .Include(x => x.CaseNotes)
                    .Include(x => x.ResidentEntity)
                    .Where(queryPostCode)
                    .Where(queryFirstName)
                    .Where(queryLastName)
                    .Where(queryHelpNeeded)
                    .ToList()
                    .ToHelpRequestWithResidentDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("SearchHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public HelpRequest PatchHelpRequest(int id, PatchHelpRequest command)
        {
            try
            {
                var rec = _helpRequestsContext.HelpRequestEntities.SingleOrDefault(x => x.Id == id);
                if (command == null)
                {
                    throw new Exception("Record not found.");
                }

                if (command.GettingInTouchReason != null)
                {
                    rec.GettingInTouchReason = command.GettingInTouchReason;
                }

                if (command.HelpWithAccessingFood != null)
                {
                    rec.HelpWithAccessingFood = command.HelpWithAccessingFood;
                }

                if (command.HelpWithAccessingSupermarketFood != null)
                {
                    rec.HelpWithAccessingSupermarketFood = command.HelpWithAccessingSupermarketFood;
                }

                if (command.HelpWithCompletingNssForm != null)
                {
                    rec.HelpWithCompletingNssForm = command.HelpWithCompletingNssForm;
                }

                if (command.HelpWithShieldingGuidance != null)
                {
                    rec.HelpWithShieldingGuidance = command.HelpWithShieldingGuidance;
                }

                if (command.HelpWithNoNeedsIdentified != null)
                {
                    rec.HelpWithNoNeedsIdentified = command.HelpWithNoNeedsIdentified;
                }

                if (command.HelpWithAccessingMedicine != null)
                {
                    rec.HelpWithAccessingMedicine = command.HelpWithAccessingMedicine;
                }

                if (command.HelpWithAccessingOtherEssentials != null)
                {
                    rec.HelpWithAccessingOtherEssentials = command.HelpWithAccessingOtherEssentials;
                }

                if (command.HelpWithDebtAndMoney != null)
                {
                    rec.HelpWithDebtAndMoney = command.HelpWithDebtAndMoney;
                }

                if (command.HelpWithMentalHealth != null)
                {
                    rec.HelpWithMentalHealth = command.HelpWithMentalHealth;
                }

                if (command.HelpWithHealth != null)
                {
                    rec.HelpWithHealth = command.HelpWithHealth;
                }

                if (command.HelpWithAccessingInternet != null)
                {
                    rec.HelpWithAccessingInternet = command.HelpWithAccessingInternet;
                }

                if (command.HelpWithSomethingElse != null)
                {
                    rec.HelpWithSomethingElse = command.HelpWithSomethingElse;
                }

                if (command.CurrentSupport != null)
                {
                    rec.CurrentSupport = command.CurrentSupport;
                }

                if (command.CurrentSupportFeedback != null)
                {
                    rec.CurrentSupportFeedback = command.CurrentSupportFeedback;
                }

                if (command.AdviceNotes != null)
                {
                    rec.AdviceNotes = command.AdviceNotes;
                }

                if (command.InitialCallbackCompleted != null)
                {
                    rec.InitialCallbackCompleted = command.InitialCallbackCompleted;
                }

                if (command.CallbackRequired != null)
                {
                    rec.CallbackRequired = command.CallbackRequired;
                }

                if (command.HelpNeeded != null)
                {
                    rec.HelpNeeded = command.HelpNeeded;
                }

                _helpRequestsContext.SaveChanges();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("PatchResidentAndHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }

            return _helpRequestsContext.HelpRequestEntities.Find(id).ToDomain();
        }

        public ResidentCaseNote PatchCaseNote(int helpRequestId, int residentId, string caseNote)
        {
            try
            {
                var rec = _helpRequestsContext.CaseNoteEntities.FirstOrDefault(x => x.HelpRequestId == helpRequestId);
                if (rec == null)
                {
                    rec = new CaseNoteEntity();
                    rec.ResidentId = residentId;
                    rec.HelpRequestId = helpRequestId;
                    _helpRequestsContext.CaseNoteEntities.Add(rec);
                }
                if (caseNote != null)
                {
                    rec.CaseNote = caseNote;
                }
                _helpRequestsContext.SaveChanges();

                return rec.ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("PatchCaseNote error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public Resident PatchResident(int id, PatchResident command)
        {
            try
            {
                var rec = _helpRequestsContext.ResidentEntities.SingleOrDefault(x => x.Id == id);
                if (command == null)
                {
                    throw new Exception("Record not found.");
                }

                if (command.FirstName != null)
                {
                    rec.FirstName = command.FirstName;
                }

                if (command.LastName != null)
                {
                    rec.LastName = command.LastName;
                }

                if (command.DobMonth != null)
                {
                    rec.DobMonth = command.DobMonth;
                }

                if (command.DobYear != null)
                {
                    rec.DobYear = command.DobYear;
                }

                if (command.DobDay != null)
                {
                    rec.DobDay = command.DobDay;
                }

                if (command.ContactTelephoneNumber != null)
                {
                    rec.ContactTelephoneNumber = command.ContactTelephoneNumber;
                }

                if (command.ContactMobileNumber != null)
                {
                    rec.ContactMobileNumber = command.ContactMobileNumber;
                }

                if (command.EmailAddress != null)
                {
                    rec.EmailAddress = command.EmailAddress;
                }

                if (command.AddressFirstLine != null && command.PostCode != null)
                {
                    // update new address fields
                    rec.AddressFirstLine = command.AddressFirstLine;
                    rec.AddressSecondLine = command.AddressSecondLine;
                    rec.AddressThirdLine = command.AddressThirdLine;
                    rec.PostCode = command.PostCode;
                    rec.Uprn = command.Uprn;
                    rec.Ward = command.Ward;
                }

                if (command.GpSurgeryDetails != null)
                {
                    rec.GpSurgeryDetails = command.GpSurgeryDetails;
                }
                if (command.IsPharmacistAbleToDeliver != null)
                {
                    rec.IsPharmacistAbleToDeliver = command.IsPharmacistAbleToDeliver;
                }

                if (command.NameAddressPharmacist != null)
                {
                    rec.NameAddressPharmacist = command.NameAddressPharmacist;
                }

                if (command.NumberOfChildrenUnder18 != null)
                {
                    rec.NumberOfChildrenUnder18 = command.NumberOfChildrenUnder18;
                }
                if (command.NhsNumber != null)
                {
                    rec.NhsNumber = command.NhsNumber;
                }

                if (command.ConsentToShare != null)
                {
                    rec.ConsentToShare = command.ConsentToShare;
                }

                if (command.RecordStatus != null)
                {
                    rec.RecordStatus = command.RecordStatus;
                }

                _helpRequestsContext.SaveChanges();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("PatchResidentAndHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }

            return _helpRequestsContext.ResidentEntities.Find(id).ToDomain();
        }

        public List<HelpRequestWithResident> GetCallbacks(CallbackQuery command)
        {
            Expression<Func<HelpRequestEntity, bool>> queryHelpNeeded = x =>
                string.IsNullOrWhiteSpace(command.HelpNeeded)
                || x.HelpNeeded.Replace(" ", "").ToUpper().Equals(command.HelpNeeded.Replace(" ", "").ToUpper());
            try
            {
                var response = _helpRequestsContext.HelpRequestEntities.Include(x => x.HelpRequestCalls)
                    .Include(x => x.ResidentEntity)
                    .Include(x => x.CaseNotes)
                    .Where(x => (x.CallbackRequired == true || x.CallbackRequired == null ||
                                 (x.InitialCallbackCompleted == false && x.CallbackRequired == false)))
                    .Where(queryHelpNeeded)
                    .OrderByDescending(x => x.InitialCallbackCompleted)
                    .ThenBy(x => x.DateTimeRecorded)
                    .ToList();
                return response.ToHelpRequestWithResidentDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetCallbacks error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        //        private void SetRecordStatus(HelpRequestEntityOld request)
        //        {
        //            request.RecordStatus = "MASTER";
        //            var duplicates = _helpRequestsContext.HelpRequestEntities
        //                .Where(x => x.Uprn == request.Uprn && x.DobMonth == request.DobMonth
        //                                                   && x.DobDay == request.DobDay && x.DobYear == request.DobYear &&
        //                                                   x.ContactTelephoneNumber == request.ContactTelephoneNumber &&
        //                                                   x.ContactMobileNumber == request.ContactMobileNumber).ToList();
        //            if (duplicates.Count > 0)
        //            {
        //                foreach (var record in duplicates)
        //                {
        //                    var rec = _helpRequestsContext.HelpRequestEntities.Find(record.Id);
        //                    rec.RecordStatus = "DUPLICATE";
        //                }
        //            }
        //            else
        //            {
        //                var exceptions = _helpRequestsContext.HelpRequestEntities
        //                    .Where(x => x.Uprn == request.Uprn).ToList();
        //                if (exceptions.Count > 0)
        //                {
        //                    request.RecordStatus = "EXCEPTION";
        //                }
        //            }
        //        }
    }
}

using System;
using System.Linq;
using Amazon.Lambda.Core;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace cv19ResSupportV3.V3.Gateways
{
    public class ResidentGateway : IResidentGateway
    {
        private readonly HelpRequestsContext _helpRequestsContext;

        public ResidentGateway(HelpRequestsContext helpRequestsContext)
        {
            _helpRequestsContext = helpRequestsContext;
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

        public Resident UpdateResident(int residentId, UpdateResident command)
        {
            try
            {
                var entity = _helpRequestsContext.ResidentEntities.Find(residentId);
                _helpRequestsContext.Entry(entity).CurrentValues.SetValues(command);
                _helpRequestsContext.SaveChanges();
                return entity.ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("UpdateResident error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }
    }
}

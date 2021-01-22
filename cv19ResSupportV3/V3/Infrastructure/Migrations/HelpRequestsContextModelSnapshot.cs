﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Infrastructure.Migrations
{
    [DbContext(typeof(HelpRequestsContext))]
    partial class HelpRequestsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("cv19ResSupportV3.V3.Infrastructure.CaseNoteEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CaseNote")
                        .HasColumnName("case_notes")
                        .HasColumnType("character varying");

                    b.Property<int>("HelpRequestId")
                        .HasColumnName("help_request_id")
                        .HasColumnType("integer");

                    b.Property<int>("ResidentId")
                        .HasColumnName("resident_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HelpRequestId");

                    b.HasIndex("ResidentId");

                    b.ToTable("resident_case_notes");
                });

            modelBuilder.Entity("cv19ResSupportV3.V3.Infrastructure.HelpRequestCallEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CallDateTime")
                        .HasColumnName("call_date_time")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CallDirection")
                        .HasColumnName("call_direction")
                        .HasColumnType("character varying");

                    b.Property<string>("CallHandler")
                        .HasColumnName("call_handler")
                        .HasColumnType("character varying");

                    b.Property<string>("CallOutcome")
                        .HasColumnName("call_outcome")
                        .HasColumnType("character varying");

                    b.Property<string>("CallType")
                        .HasColumnName("call_type")
                        .HasColumnType("character varying");

                    b.Property<int>("HelpRequestId")
                        .HasColumnName("help_request_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HelpRequestId");

                    b.ToTable("help_request_calls");
                });

            modelBuilder.Entity("cv19ResSupportV3.V3.Infrastructure.HelpRequestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AdviceNotes")
                        .HasColumnName("advice_notes")
                        .HasColumnType("character varying");

                    b.Property<string>("AssignedTo")
                        .HasColumnName("assigned_to")
                        .HasColumnType("character varying");

                    b.Property<bool?>("CallbackRequired")
                        .HasColumnName("callback_required")
                        .HasColumnType("bool");

                    b.Property<bool?>("ConsentToCompleteOnBehalf")
                        .HasColumnName("consent_to_complete_on_behalf")
                        .HasColumnType("bool");

                    b.Property<string>("CurrentSupport")
                        .HasColumnName("current_support")
                        .HasColumnType("character varying");

                    b.Property<string>("CurrentSupportFeedback")
                        .HasColumnName("current_support_feedback")
                        .HasColumnType("character varying");

                    b.Property<DateTime?>("DateTimeRecorded")
                        .HasColumnName("date_time_recorded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("GettingInTouchReason")
                        .HasColumnName("getting_in_touch_reason")
                        .HasColumnType("character varying");

                    b.Property<string>("HelpNeeded")
                        .HasColumnName("help_needed")
                        .HasColumnType("character varying");

                    b.Property<bool?>("HelpWithAccessingFood")
                        .HasColumnName("help_with_accessing_food")
                        .HasColumnType("bool");

                    b.Property<bool?>("HelpWithAccessingInternet")
                        .HasColumnName("help_with_accessing_internet")
                        .HasColumnType("bool");

                    b.Property<bool?>("HelpWithAccessingMedicine")
                        .HasColumnName("help_with_accessing_medicine")
                        .HasColumnType("bool");

                    b.Property<bool?>("HelpWithAccessingOtherEssentials")
                        .HasColumnName("help_with_accessing_other_essentials")
                        .HasColumnType("bool");

                    b.Property<bool?>("HelpWithAccessingSupermarketFood")
                        .HasColumnName("help_with_accessing_supermarket_food")
                        .HasColumnType("bool");

                    b.Property<bool?>("HelpWithChildrenAndSchools")
                        .HasColumnName("help_with_children_and_schools")
                        .HasColumnType("bool");

                    b.Property<bool?>("HelpWithCompletingNssForm")
                        .HasColumnName("help_with_completing_nss_form")
                        .HasColumnType("bool");

                    b.Property<bool?>("HelpWithDebtAndMoney")
                        .HasColumnName("help_with_debt_and_money")
                        .HasColumnType("bool");

                    b.Property<bool?>("HelpWithDisabilities")
                        .HasColumnName("help_with_disabilities")
                        .HasColumnType("bool");

                    b.Property<bool?>("HelpWithHealth")
                        .HasColumnName("help_with_health")
                        .HasColumnType("bool");

                    b.Property<bool?>("HelpWithHousing")
                        .HasColumnName("help_with_housing")
                        .HasColumnType("bool");

                    b.Property<bool?>("HelpWithJobsOrTraining")
                        .HasColumnName("help_with_jobs_or_training")
                        .HasColumnType("bool");

                    b.Property<bool?>("HelpWithMentalHealth")
                        .HasColumnName("help_with_mental_health")
                        .HasColumnType("bool");

                    b.Property<bool?>("HelpWithNoNeedsIdentified")
                        .HasColumnName("help_with_no_needs_identified")
                        .HasColumnType("bool");

                    b.Property<bool?>("HelpWithShieldingGuidance")
                        .HasColumnName("help_with_shielding_guidance")
                        .HasColumnType("bool");

                    b.Property<bool?>("HelpWithSomethingElse")
                        .HasColumnName("help_with_something_else")
                        .HasColumnType("bool");

                    b.Property<bool?>("InitialCallbackCompleted")
                        .HasColumnName("initial_callback_completed")
                        .HasColumnType("bool");

                    b.Property<bool?>("IsOnBehalf")
                        .HasColumnName("is_on_behalf")
                        .HasColumnType("bool");

                    b.Property<bool?>("MedicineDeliveryHelpNeeded")
                        .HasColumnName("medicine_delivery_help_needed")
                        .HasColumnType("bool");

                    b.Property<string>("NhsCtasId")
                        .HasColumnName("nhs_ctas_id")
                        .HasColumnType("character varying");

                    b.Property<string>("OnBehalfContactNumber")
                        .HasColumnName("on_behalf_contact_number")
                        .HasColumnType("character varying");

                    b.Property<string>("OnBehalfEmailAddress")
                        .HasColumnName("on_behalf_email_address")
                        .HasColumnType("character varying");

                    b.Property<string>("OnBehalfFirstName")
                        .HasColumnName("on_behalf_first_name")
                        .HasColumnType("character varying");

                    b.Property<string>("OnBehalfLastName")
                        .HasColumnName("on_behalf_last_name")
                        .HasColumnType("character varying");

                    b.Property<string>("RelationshipWithResident")
                        .HasColumnName("relationship_with_resident")
                        .HasColumnType("character varying");

                    b.Property<int>("ResidentId")
                        .HasColumnName("resident_id")
                        .HasColumnType("integer");

                    b.Property<string>("UrgentEssentials")
                        .HasColumnName("urgent_essentials")
                        .HasColumnType("character varying");

                    b.Property<string>("UrgentEssentialsAnythingElse")
                        .HasColumnName("urgent_essentials_anything_else")
                        .HasColumnType("character varying");

                    b.Property<string>("WhenIsMedicinesDelivered")
                        .HasColumnName("when_is_medicines_delivered")
                        .HasColumnType("character varying");

                    b.HasKey("Id");

                    b.HasIndex("ResidentId");

                    b.ToTable("help_requests");
                });

            modelBuilder.Entity("cv19ResSupportV3.V3.Infrastructure.LookupEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Lookup")
                        .HasColumnName("lookup")
                        .HasColumnType("character varying");

                    b.Property<string>("LookupGroup")
                        .HasColumnName("lookup_group")
                        .HasColumnType("character varying");

                    b.HasKey("Id");

                    b.ToTable("inh_lookups");
                });

            modelBuilder.Entity("cv19ResSupportV3.V3.Infrastructure.ResidentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AddressFirstLine")
                        .HasColumnName("address_first_line")
                        .HasColumnType("character varying");

                    b.Property<string>("AddressSecondLine")
                        .HasColumnName("address_second_line")
                        .HasColumnType("character varying");

                    b.Property<string>("AddressThirdLine")
                        .HasColumnName("address_third_line")
                        .HasColumnType("character varying");

                    b.Property<bool?>("ConsentToShare")
                        .HasColumnName("consent_to_share")
                        .HasColumnType("bool");

                    b.Property<string>("ContactMobileNumber")
                        .HasColumnName("contact_mobile_number")
                        .HasColumnType("character varying");

                    b.Property<string>("ContactTelephoneNumber")
                        .HasColumnName("contact_telephone_number")
                        .HasColumnType("character varying");

                    b.Property<string>("DobDay")
                        .HasColumnName("dob_day")
                        .HasColumnType("character varying");

                    b.Property<string>("DobMonth")
                        .HasColumnName("dob_month")
                        .HasColumnType("character varying");

                    b.Property<string>("DobYear")
                        .HasColumnName("dob_year")
                        .HasColumnType("character varying");

                    b.Property<string>("EmailAddress")
                        .HasColumnName("email_address")
                        .HasColumnType("character varying");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name")
                        .HasColumnType("character varying");

                    b.Property<string>("GpSurgeryDetails")
                        .HasColumnName("gp_surgery_details")
                        .HasColumnType("character varying");

                    b.Property<bool?>("IsPharmacistAbleToDeliver")
                        .HasColumnName("is_pharmacist_able_to_deliver")
                        .HasColumnType("bool");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name")
                        .HasColumnType("character varying");

                    b.Property<string>("NameAddressPharmacist")
                        .HasColumnName("name_address_pharmacist")
                        .HasColumnType("character varying");

                    b.Property<string>("NhsNumber")
                        .HasColumnName("nhs_number")
                        .HasColumnType("character varying");

                    b.Property<string>("NumberOfChildrenUnder18")
                        .HasColumnName("number_of_children_under_18")
                        .HasColumnType("character varying");

                    b.Property<string>("PostCode")
                        .HasColumnName("postcode")
                        .HasColumnType("character varying");

                    b.Property<string>("RecordStatus")
                        .HasColumnName("record_status")
                        .HasColumnType("character varying");

                    b.Property<string>("Uprn")
                        .HasColumnName("uprn")
                        .HasColumnType("character varying");

                    b.Property<string>("Ward")
                        .HasColumnName("ward")
                        .HasColumnType("character varying");

                    b.HasKey("Id");

                    b.ToTable("residents");
                });

            modelBuilder.Entity("cv19ResSupportV3.V3.Infrastructure.CaseNoteEntity", b =>
                {
                    b.HasOne("cv19ResSupportV3.V3.Infrastructure.HelpRequestEntity", "HelpRequestEntity")
                        .WithMany("CaseNotes")
                        .HasForeignKey("HelpRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cv19ResSupportV3.V3.Infrastructure.ResidentEntity", "ResidentEntity")
                        .WithMany("CaseNotes")
                        .HasForeignKey("ResidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cv19ResSupportV3.V3.Infrastructure.HelpRequestCallEntity", b =>
                {
                    b.HasOne("cv19ResSupportV3.V3.Infrastructure.HelpRequestEntity", "HelpRequestEntity")
                        .WithMany("HelpRequestCalls")
                        .HasForeignKey("HelpRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cv19ResSupportV3.V3.Infrastructure.HelpRequestEntity", b =>
                {
                    b.HasOne("cv19ResSupportV3.V3.Infrastructure.ResidentEntity", "ResidentEntity")
                        .WithMany("HelpRequests")
                        .HasForeignKey("ResidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

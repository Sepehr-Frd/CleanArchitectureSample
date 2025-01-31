﻿using Application.Common.Constants;

namespace Web.GraphQL.Types;

public sealed class MutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Authorize()
            .Description("Root Mutation That Provides Operations to Modify Data");

        descriptor
            .Field(_ => Mutation.LoginAsync(null!, null!, CancellationToken.None))
            .AllowAnonymous()
            .Description("Allows users to log in and obtain authentication credentials.\n" +
                         "Requires valid username/email and password.\n" +
                         "Returns an authentication token upon successful login.");

        descriptor
            .Field(_ =>
                Mutation.SignUpAsync(null!, null!, CancellationToken.None))
            .AllowAnonymous()
            .Description("Registers a new user account.\n" +
                         "Requires user details such as username, email and password.");

        descriptor
            .Field(_ =>
                Mutation.AddAddressAsync(null!, null!, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description(
                "Adds a new address to the user's profile. Requires details such as street, city, state, postal code, etc.\n" +
                "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.UpdateAddressAsync(null!, 0, null!, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description(
                "Updates an existing address in the user's profile. Requires the address ID and updated details.\n" +
                "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.DeleteAddressAsync(null!, 0, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Deletes an address from the user's profile. Requires the address ID.\n" +
                         "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.AddAnswerAsync(null!, null!, null!, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Adds a new answer to a question. Requires the question ID and the content of the answer.\n" +
                         "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.UpdateAnswerAsync(null!, 0, null!, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Updates an existing answer. Requires the answer ID and the updated content.\n" +
                         "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.DeleteAnswerAsync(null!, 0, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Deletes an answer and all of its votes.\n" +
                         "Requires the answer ID.\n" +
                         "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.AddCartItemAsync(null!, null!, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Adds a product to the user's shopping cart. Requires the product ID and the quantity.\n" +
                         "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.DeleteCartItemAsync(null!, 0, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Removes a product from the user's shopping cart.\n" +
                         "Requires the product ID. Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.AddCategoryAsync(null!, null!, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description("Adds a new product category.\n" +
                         "Requires details such as category name and description.\n" +
                         "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.AddCommentAsync(null!, null!, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Adds a new comment to a product.\n" +
                         "Requires the target ID and the content of the comment.\n" +
                         "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.DeleteCommentAsync(null!, 0, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Deletes a comment and all of its votes.\n" +
                         "Requires the comment ID.\n" +
                         "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.AddOrderAsync(null!, null!, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Places a new order.\n" +
                         "Requires product details, quantities, and payment information.\n" +
                         "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.UpdateOrderAsync(null!, 0, null!, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description("Updates an existing order.\n" +
                         "Requires the order ID and updated details.\n" +
                         "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.DeleteOrderAsync(null!, 0, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description(
                "Deletes an order and all of its related entities such as order items, payment, shipment, addresses, etc.\n" +
                "Requires the order ID.\n" +
                "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.UpdatePaymentAsync(null!, 0, null!, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description("Updates the payment details for an order.\n" +
                         "Requires the order ID and updated payment information.\n" +
                         "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.UpdatePersonAsync(null!, 0, null!, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description("Updates user profile information.\n" +
                         "Requires the user ID and updated details.\n" +
                         "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.AddPhoneNumberAsync(null!, null!, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Adds a new phone number to the user's profile.\n" +
                         "Requires the phone number.\n" +
                         "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.UpdatePhoneNumberAsync(null!, 0, null!, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Updates an existing phone number in the user's profile.\n" +
                         "Requires the phone number ID and updated details.\n" +
                         "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.DeletePhoneNumberAsync(null!, 0, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Deletes a phone number from the user's profile.\n" +
                         "Requires the phone number ID.\n" +
                         "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.AddProductAsync(null!, null!, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description("Adds a new product.\n" +
                         "Requires product details such as name, description, price, etc.\n" +
                         "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.UpdateProductAsync(null!, 0, null!, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description("Updates an existing product.\n" +
                         "Requires the product ID and updated details.\n" +
                         "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.DeleteProductAsync(null!, 0, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description(
                "Deletes a product and all of its related entities such as comments, votes, questions, answers, etc.\n" +
                "Requires the product ID.\n" +
                "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.AddQuestionAsync(null!, null!, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Adds a new question to a product.\n" +
                         "Requires details such as title, description, and product ID.\n" +
                         "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.DeleteQuestionAsync(null!, 0, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Deletes a question and all of its related entities such as answers, votes, etc.\n" +
                         "Requires the question ID.\n" +
                         "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.AddRoleAsync(null!, null!, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description("Adds a new role.\n" +
                         "Requires details such as title and description.\n" +
                         "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.UpdateRoleAsync(null!, 0, null!, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description("Updates an existing role.\n" +
                         "Requires the role ID and updated details.\n" +
                         "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.DeleteRoleAsync(null!, 0, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description("Deletes a role and its user-roles.\n" +
                         "Requires the role ID.\n" +
                         "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.UpdateShipmentAsync(null!, 0, null!, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description("Updates shipment details.\n" +
                         "Requires the shipment ID and updated details.\n" +
                         "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.AddUserRoleAsync(null!, null!, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description("Assigns a role to a user.\n" +
                         "Requires the user ID and role ID.\n" +
                         "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.DeleteUserRoleAsync(null!, 0, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description("Removes a role from a user.\n" +
                         "Requires the user-role ID.\n" +
                         "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.UpdateUserAsync(null!, null!, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description("Updates user details.\n" +
                         "Requires the user ID and updated information.\n" +
                         "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.DeleteUserAsync(null!, 0, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description(
                "Deletes a user account and all of its related entities such as questions, orders, addresses, etc.\n" +
                "Requires the user ID.\n" +
                "Authentication is required and only administrators are allowed.");

        descriptor
            .Field(_ =>
                Mutation.AddVoteAsync(null!, null!, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Creates a vote on a content (question, answer, comment and product).\n" +
                         "Requires the type of vote, content type, and content ID.\n" +
                         "Authentication is required and only customers are allowed.");

        descriptor
            .Field(_ =>
                Mutation.DeleteVoteAsync(null!, 0, CancellationToken.None))
            .Authorize(PolicyConstants.CustomerPolicy)
            .Description("Removes a vote from a content.\n" +
                         "Requires the vote ID.\n" +
                         "Authentication is required and only customers are allowed.");
    }
}
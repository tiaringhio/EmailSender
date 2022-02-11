<!-- PROJECT LOGO -->
  <br />
    <p align="center">
    <img src="EmailSender\email.png" alt="Logo" width="130" height="130">
  </a>
  <h1 align="center">Email Sender</h1>
  <p align="center">
    Small project to send email via <a href="https://sendgrid.com/" >SendGrid</a> using an Azure Function 4.0 running in a Docker container.
  </p>

## Table of contents

- [Table of contents](#table-of-contents)
- [About the project](#about-the-project)
- [Used in this project](#used-in-this-project)
  - [Azure Function 4.0](#azure-function)
  - [Docker](#docker)
  - [SendGrid](#sendgrid)
- [How To](#how-to)
- [License](#license)
- [Contributors](#contributors)

# About The Project

This project aims to create a small Web Service that consists in an Azure Function 4.0 with ah HTTP Trigger running in a Docker cointainer that sends email via the SendGrid C# SDK.

This project is an "academic excercise" that I used to try out the recently released Azure Functions 4.0 (which use .NET 6)

# Used in this project

## Azure Function

You cand read more about the capabilities of Azure Functions [Here](https://techcommunity.microsoft.com/t5/apps-on-azure-blog/azure-functions-4-0-and-net-6-support-are-now-generally/ba-p/2933245). They'are very useful in a microservice environment, they are usually small pieces of software (yup, that's what a microservice is... [Kind Of](https://microservices.io/)).

The Azure Function i'm using uses and HTTP trigger, which means that the Function wakes up when it receives an HTTP request, runs some tasks then goes back to sleep, pretty easy.

## Docker

Yup, Docker for sending a simple email, you read that right.

## SendGrid

SendGrid is a cloud-based SMTP service provider which let you use their email server to send your emails, you get 100 emails a day on the free tier, more than enough for playing around. The paid tiers are very also honest in terms of pricing.

Why the heck would I use SendGrid? Well in a cloud environment you usually cannot send email via STMP in a simple way, also who wants to manage an email server?

SendGrid offers an [C# SDK](https://github.com/sendgrid/sendgrid-csharp) to send emails with a few lines of code, I used more lines of code to setup the environment than to send the actual email.

# How to

Create and `.env` file (used to put you sendgrid API key and so on) like the following:

```dosini
SendGridKey=<API-GOES-HERE>
SendGridSender=<SENDER-GOES-HERE>
SendGridSenderName=<SENDER-NAME-GOES-HERE>
Receiver=<RECEIVER-GOES-HERE>
```

Where do I find those variables? In your SendGrid dashboard after creating an account.

Then run the Docker image passing the `.env` file like so:

```docker
docker run -dp 5000:80 --env-file .env ghcr.io/tiaringhio/emailsender:latest
```

This will run the server on port 5000, change that if you want/need.

you can the send HTTP Post requests followinf the [EmailData](./EmailSender/EmailData.cs) class.

Example in JSON:

```JSON
{
    "sender": "Mattia Ricci",
    "subject": "Testing email",
    "message": "This mail was sent in a very complicated (and uneccessary) way"
}
```

Have fun!

# Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

Shamefully copied from [montali](https://github.com/montali)

# License

Distributed under the GPL License. See `LICENSE` for more information.

Icons made by <a href="https://www.flaticon.com/authors/smashicons" title="Smashicons">Smashicons</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a>

# Contributors

[Mattia Ricci](https://mattiaricci.it)

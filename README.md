# reverse-proxy-kestrel

Sample reverse-proxy setups for use with a Kestrel app.

Why did I develop this project? There seems to be a few claims** on the web that say Microsoft recommends using Kestrel
along with a reverse proxy. Understandably so, since Kestrel was specifically designed to be lightweight.

** some examples:
  - [Oreilly.com](https://www.oreilly.com/library/view/mastering-aspnet-web/9781786463951/5984f119-6ac4-4316-a5ec-5ad9587f6760.xhtml)
  - [Linode.com](https://www.linode.com/docs/guides/tutorial-host-asp-net-core-on-linux/#deploy-your-application-with-nginx)
  - [Tutorialspoint.com](https://www.tutorialspoint.com/what-is-kestrel-and-how-does-it-differ-from-iis-asp-net)
  - [DotcoreTutorials.com](https://dotnetcoretutorials.com/2019/12/25/kestrel-vs-iis/)
  - Point # 16 here: [Interviewbit.com](https://www.interviewbit.com/asp-net-interview-questions/#:~:text=Though%20Kestrel%20can%20serve%20an,performance%2C%20security%2C%20and%20reliability.)
  - [partech.nl](https://www.partech.nl/en/publications/2021/10/kestrel-vs-iis-web-servers#)

It's no longer necessarily the case that Microsoft advises against using Kestrel directly exposed to the Internet,
however, there are still reasons you may want to use Kestrel behind a reverse proxy.

---

## Why use a reverse proxy instead of just Kestrel?

1. Load balancing
2. Caching static content
3. DDoS protection
4. SSL termination
5. Insights and logging
6. Simplify deployment
7. Stuck having to use older version of .Net core (hence older version of Kestrel, which bad rhetoric applies to)

## Some features lacking in Kestrel that you (might) run into

- Doesn't have robust Mime-Type mapping.
- Request Filtering (e.g. Blocking access to certain file extensions, folders, verbs etc).
- HTTP access logs aren’t collected.
- Multiple apps on the same port.

More reading to help you decide:

- [IIS and Kestrel feature comparison](https://stackify.com/kestrel-web-server-asp-net-core-kestrel-vs-iis/)
- [When to use Kestrel with a reverse proxy](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/when-to-use-a-reverse-proxy?view=aspnetcore-6.0)
- [Configure ASP.NET Core to work with proxy servers and load balancers](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-6.0)

---

## Getting started with these examples

#### Assumptions

 - You're using Linux
 - You have basic docker knowledge
 - You have Docker installed

The .Net app is in the `app` folder. It's just a very simple app that was autogenerated using `dotnet new webapi` and
includes the GET `/weatherforecast` endpoint. I've also added "Hello Word" at (GET) `/hello`.

The [`app/docker-build.sh`](app/docker-build.sh) script will build a docker image named `demo`, from the app code. This
image must be built first before trying to use any of the Docker compose files, which depend on the image being available.

After building the `demo` image, run the app behind one of the proxy servers using docker compose. The reverse proxy
examples are for the following types `apache`, `haproxy`, `nginx`, `traefik` using this following command format:

    docker compose -f ./compose-<type>.yml up

For example: `docker compose -f ./compose-apache.yml up`  
... and to bring it down: `docker compose -f ./compose-apache.yml down`

---

## Pros and Cons

### Apache

https://httpd.apache.org/

| Pros                                                              | Cons                                      |
|-------------------------------------------------------------------|-------------------------------------------|
| Popular, proven, well documented and supported. Lots of features. | Complex configuration; not purpose built. |

---

### HaProxy

https://www.haproxy.org/

| Pros                                                                     | Cons                      |
|--------------------------------------------------------------------------|---------------------------|
| Purpose built, *very fast*, popular. Basic reporting UI. Fault tolerant. | Odd configuration. Dated. |

---

### Nginx

https://www.nginx.com/

| Pros                  | Cons                                                 |
|-----------------------|------------------------------------------------------|
| Fast, popular, simple | Free vs Paid version, not GUI and not purpose built. |

---

### Traefik Proxy

https://traefik.io/traefik/

| Pros                                                             | Cons                          |
|------------------------------------------------------------------|-------------------------------|
| Fast, simple, modern, purpose built, built-in GUI, "rising star" | None that I've seen (so far). |


---

## Take Away

Each option has its own considerations, but the most outstanding consideration means that we really have just 2 categories
of options: purpose-built reverse proxies which are fast but can't do much else, or web servers with reverse proxy
capabilities that can be used to create complex solutions.

If in doubt, there's no reason you couldn't use one of the web servers behind a purpose built proxy, along with your
Kestrel app.
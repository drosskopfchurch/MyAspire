export const dynamic = 'force-static'

export async function GET(req) {
    const { url } = req;
    const apiservice = process.env.services__apiservice__https__0 ||
        process.env.services__apiservice__http__0
    // console.log(JSON.stringify(req))
    const fetchUrl = `${apiservice}/${url.replace('http://localhost:3000/api-service/', '')}`
    console.log(fetchUrl)
    const res = await fetch(`${fetchUrl}`)
    const data = await res.json()

    return Response.json({ data })
}
export const dynamic = 'force-static'

export async function GET(req) {
    const { url } = req;
    const apiservice = process.env.services__apigame__https__0 ||
        process.env.services__apigame__http__0
    console.log(`ApiService: ${apiservice}`);
    const fetchUrl = `${apiservice}/${url.replace('http://localhost:3000/api-game/', '')}`
    console.log(fetchUrl)
    const res = await fetch(`${fetchUrl}`)
    const data = await res.json()

    return Response.json({ data })
}
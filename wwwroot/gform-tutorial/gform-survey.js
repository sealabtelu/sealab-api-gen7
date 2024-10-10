const POST_URL = "https://api-local.sealab-telu.com/gform-survey"
function onSubmit(e) {
    const form = FormApp.getActiveForm()
    const allResponses = form.getResponses()
    const latestResponse = allResponses[allResponses.length - 1]
    const response = latestResponse.getItemResponses()
    let payload = {}
    for (let i = 0; i < response.length; i++) {
        const question = response[i].getItem().getTitle()
        const answer = response[i].getResponse()
        payload[question] = answer
    }
    const token = payload["Token"]
    delete payload["Token"]
    payload = JSON.stringify(payload)
    const options = {
        method: "post",
        contentType: "application/json",
        payload: JSON.stringify({ response: payload }),
        headers: {
            Authorization: `bearer ${token}`
        }
    }
    UrlFetchApp.fetch(POST_URL, options)
};
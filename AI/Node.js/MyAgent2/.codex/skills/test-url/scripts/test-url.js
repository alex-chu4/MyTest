export async function get(args) {
  const { url, headers = {} } = args;

  try {
    //console.log(url);
    const res = await fetch(url, { method: "GET", headers });
    const text = await res.text();

    return {
      status: res.status,
      ok: res.ok,
      headers: Object.fromEntries(res.headers.entries()),
      body: tryParseJSON(text)
    };
  } catch (err) {
    return { error: err.message };
  }
}

export async function post(args) {
  const { url, body = "", headers = {} } = args;

  try {
    const res = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        ...headers
      },
      body
    });

    const text = await res.text();

    return {
      status: res.status,
      ok: res.ok,
      headers: Object.fromEntries(res.headers.entries()),
      body: tryParseJSON(text)
    };
  } catch (err) {
    return { error: err.message };
  }
}

function tryParseJSON(text) {
  try {
    return JSON.parse(text);
  } catch {
    return text;
  }
}

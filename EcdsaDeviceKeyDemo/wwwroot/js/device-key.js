(() => {
    "use strict";

    const DB_NAME = "ecdsa-device-key-demo";
    const DB_VERSION = 1;
    const STORE_NAME = "key-pairs";
    const KEY_NAME = "current";
    const DEVICE_ID_KEY = "ecdsa-device-key-demo.device-id";

    const state = {
        deviceId: getOrCreateDeviceId(),
        keyPair: null,
        publicKeyBase64: null,
        registered: false
    };

    const $ = (id) => document.getElementById(id);

    function getOrCreateDeviceId() {
        let deviceId = window.localStorage.getItem(DEVICE_ID_KEY);
        if (!deviceId) {
            deviceId = crypto.randomUUID();
            window.localStorage.setItem(DEVICE_ID_KEY, deviceId);
        }
        return deviceId;
    }

    function setStatus(message, kind = "info") {
        const element = $("status");
        element.textContent = message;
        element.className = `status ${kind}`;
    }

    function appendLog(label, value) {
        const log = $("log");
        const timestamp = new Date().toLocaleTimeString();
        const text = typeof value === "string" ? value : JSON.stringify(value, null, 2);
        log.textContent = `[${timestamp}] ${label}\n${text}\n\n${log.textContent}`.trim();
    }

    function updateUi() {
        $("device-id").textContent = state.deviceId;
        $("key-state").textContent = state.keyPair ? "ECDSA P-256 · 私鑰不可匯出" : "尚未載入";
        $("registration-state").textContent = state.registered ? "已註冊" : "尚未註冊";
        $("prepare-key").disabled = false;
        $("register-key").disabled = !state.keyPair;
        $("prove-key").disabled = !state.keyPair || !state.registered;
    }

    function openKeyDatabase() {
        return new Promise((resolve, reject) => {
            const request = window.indexedDB.open(DB_NAME, DB_VERSION);
            request.onerror = () => reject(request.error ?? new Error("無法開啟 IndexedDB。"));
            request.onupgradeneeded = () => {
                if (!request.result.objectStoreNames.contains(STORE_NAME)) {
                    request.result.createObjectStore(STORE_NAME);
                }
            };
            request.onsuccess = () => resolve(request.result);
        });
    }

    async function readStoredKeyPair() {
        const db = await openKeyDatabase();
        return new Promise((resolve, reject) => {
            const transaction = db.transaction(STORE_NAME, "readonly");
            const request = transaction.objectStore(STORE_NAME).get(KEY_NAME);
            request.onerror = () => reject(request.error ?? new Error("無法讀取本機金鑰。"));
            request.onsuccess = () => resolve(request.result ?? null);
            transaction.oncomplete = () => db.close();
        });
    }

    async function saveKeyPair(keyPair) {
        const db = await openKeyDatabase();
        return new Promise((resolve, reject) => {
            const transaction = db.transaction(STORE_NAME, "readwrite");
            transaction.objectStore(STORE_NAME).put(keyPair, KEY_NAME);
            transaction.onerror = () => reject(transaction.error ?? new Error("無法保存本機金鑰。"));
            transaction.oncomplete = () => {
                db.close();
                resolve();
            };
        });
    }

    async function deleteStoredKeyPair() {
        const db = await openKeyDatabase();
        return new Promise((resolve, reject) => {
            const transaction = db.transaction(STORE_NAME, "readwrite");
            transaction.objectStore(STORE_NAME).delete(KEY_NAME);
            transaction.onerror = () => reject(transaction.error ?? new Error("無法清除本機金鑰。"));
            transaction.oncomplete = () => {
                db.close();
                resolve();
            };
        });
    }

    function toBase64(bytes) {
        let binary = "";
        const chunkSize = 0x8000;
        for (let index = 0; index < bytes.length; index += chunkSize) {
            binary += String.fromCharCode(...bytes.subarray(index, index + chunkSize));
        }
        return window.btoa(binary);
    }

    async function createOrLoadKeyPair() {
        if (!window.isSecureContext || !window.crypto?.subtle) {
            throw new Error("WebCrypto 需要安全環境；請使用 HTTPS 或 localhost。 ");
        }

        try {
            const stored = await readStoredKeyPair();
            if (stored?.privateKey && stored?.publicKey) {
                return stored;
            }
        } catch (error) {
            appendLog("IndexedDB 載入略過", error.message);
        }

        const keyPair = await window.crypto.subtle.generateKey(
            { name: "ECDSA", namedCurve: "P-256" },
            false,
            ["sign", "verify"]
        );

        try {
            await saveKeyPair(keyPair);
        } catch (error) {
            appendLog("IndexedDB 保存略過（本次頁面仍可使用）", error.message);
        }
        return keyPair;
    }

    async function prepareKey() {
        try {
            setStatus("正在產生或載入 ECDSA P-256 金鑰…");
            state.keyPair = await createOrLoadKeyPair();
            const spki = await window.crypto.subtle.exportKey("spki", state.keyPair.publicKey);
            state.publicKeyBase64 = toBase64(new Uint8Array(spki));
            state.registered = false;
            updateUi();
            setStatus("金鑰已就緒。下一步請註冊 Public Key。", "success");
            appendLog("Key ready", {
                algorithm: state.keyPair.publicKey.algorithm,
                extractablePrivateKey: state.keyPair.privateKey.extractable,
                publicKeyFormat: "SPKI / Base64"
            });
        } catch (error) {
            setStatus(error.message, "error");
            appendLog("Key error", error.message);
        }
    }

    async function callApi(url, options = {}) {
        const response = await window.fetch(url, {
            ...options,
            headers: {
                Accept: "application/json",
                ...(options.body ? { "Content-Type": "application/json" } : {}),
                ...(options.headers ?? {})
            }
        });
        let body = null;
        try {
            body = await response.json();
        } catch {
            // Keep the HTTP status as the useful error when the server did not return JSON.
        }
        if (!response.ok) {
            throw new Error(body?.message ?? `API 回應 HTTP ${response.status}`);
        }
        return body;
    }

    async function registerKey() {
        if (!state.keyPair || !state.publicKeyBase64) {
            setStatus("請先產生／載入金鑰。", "error");
            return;
        }

        try {
            setStatus("正在註冊 Public Key…");
            const result = await callApi("/api/device/register", {
                method: "POST",
                body: JSON.stringify({ deviceId: state.deviceId, publicKey: state.publicKeyBase64 })
            });
            state.registered = true;
            updateUi();
            setStatus(result.message, "success");
            appendLog("POST /api/device/register", result);
        } catch (error) {
            setStatus(error.message, "error");
            appendLog("Register error", error.message);
        }
    }

    async function proveKey() {
        if (!state.keyPair || !state.registered) {
            setStatus("請先完成金鑰建立與 Public Key 註冊。", "error");
            return;
        }

        try {
            setStatus("正在取得一次性 Challenge…");
            const challenge = await callApi(`/api/device/challenge?deviceId=${encodeURIComponent(state.deviceId)}`);
            const data = new TextEncoder().encode(challenge.messageToSign);
            const signature = await window.crypto.subtle.sign(
                { name: "ECDSA", hash: "SHA-256" },
                state.keyPair.privateKey,
                data
            );

            setStatus("正在送出 Signature，Server 以 C# 驗證…");
            const result = await callApi("/api/device/verify", {
                method: "POST",
                body: JSON.stringify({
                    deviceId: state.deviceId,
                    challengeId: challenge.challengeId,
                    message: challenge.messageToSign,
                    signature: toBase64(new Uint8Array(signature))
                })
            });

            $("verification-state").textContent = result.ok ? "成功 · Challenge 已消耗" : "失敗";
            setStatus(result.message, result.ok ? "success" : "error");
            appendLog("POST /api/device/verify", result);
        } catch (error) {
            $("verification-state").textContent = "失敗";
            setStatus(error.message, "error");
            appendLog("Verify error", error.message);
        }
    }

    async function clearKey() {
        try {
            await deleteStoredKeyPair();
        } catch (error) {
            appendLog("IndexedDB 清除錯誤", error.message);
        }
        window.localStorage.removeItem(DEVICE_ID_KEY);
        window.location.reload();
    }

    $("prepare-key").addEventListener("click", prepareKey);
    $("register-key").addEventListener("click", registerKey);
    $("prove-key").addEventListener("click", proveKey);
    $("clear-key").addEventListener("click", clearKey);
    updateUi();
})();

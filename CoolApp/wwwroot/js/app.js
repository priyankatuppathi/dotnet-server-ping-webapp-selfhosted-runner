// Live local clock
function tick() {
  document.getElementById("clock").textContent =
    new Date().toLocaleTimeString();
}
setInterval(tick, 1000);
tick();

// Fetch server details and show them
async function loadServerInfo() {
  const status = document.getElementById("status");
  try {
    const res = await fetch("/api/hello");
    const data = await res.json();

    document.getElementById("message").textContent = data.message;
    document.getElementById("time").textContent = data.serverTime;
    document.getElementById("framework").textContent = data.framework;
    document.getElementById("environment").textContent = data.environment;
    document.getElementById("version").textContent = data.version;
    document.getElementById("commit").textContent =
      data.commit.length > 7 ? data.commit.substring(0, 7) : data.commit;
    document.getElementById("uptime").textContent = data.uptime;
    document.getElementById("result").classList.remove("hidden");

    status.classList.add("online");
    document.getElementById("statusText").textContent = "Online";
  } catch {
    document.getElementById("statusText").textContent = "Error";
  }
}

// Auto-load on open, refresh every 5s (keeps time + uptime live), and on click
loadServerInfo();
setInterval(loadServerInfo, 5000);
document.getElementById("pingBtn").addEventListener("click", loadServerInfo);

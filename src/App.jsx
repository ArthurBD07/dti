import { useEffect, useState } from "react";
import "./App.css";
import LeadList from "./components/LeadList";

function App() {
  const [abaAtiva, setAbaAtiva] = useState("invited"); // invited | accepted
  const [leadsInvited, setLeadsInvited] = useState([]);
  const [leadsAccepted, setLeadsAccepted] = useState([]);

  const API_URL = "http://localhost:5108";

  // Carrega os leads do backend ao iniciar
  useEffect(() => {
    fetch(`${API_URL}/leads/invited`)
      .then((res) => res.json())
      .then((data) => setLeadsInvited(data))
      .catch((err) => console.error("Erro ao carregar invited:", err));

    fetch(`${API_URL}/leads/accepted`)
      .then((res) => res.json())
      .then((data) => setLeadsAccepted(data))
      .catch((err) => console.error("Erro ao carregar accepted:", err));
  }, []);

  // ACEITAR LEAD (POST no backend .NET)
  async function handleAccept(id) {
    try {
      const res = await fetch(`${API_URL}/leads/${id}/accept`, {
        method: "POST",
      });

      if (!res.ok) {
        alert("Erro ao aceitar lead");
        return;
      }

      const leadAtualizado = await res.json();

      // Atualiza listas no front-end
      setLeadsInvited((prev) => prev.filter((l) => l.id !== id));
      setLeadsAccepted((prev) => [...prev, leadAtualizado]);

      alert(`Lead ${id} aceito com sucesso!`);
    } catch (error) {
      console.error(error);
      alert("Erro inesperado ao aceitar o lead");
    }
  }

  // RECUSAR LEAD (POST no backend .NET)
  async function handleDecline(id) {
    try {
      const res = await fetch(`${API_URL}/leads/${id}/decline`, {
        method: "POST",
      });

      if (!res.ok) {
        alert("Erro ao recusar lead");
        return;
      }

      // Remove o lead recusado da lista invited
      setLeadsInvited((prev) => prev.filter((l) => l.id !== id));

      alert(`Lead ${id} recusado.`);
    } catch (error) {
      console.error(error);
      alert("Erro inesperado ao recusar o lead");
    }
  }

  return (
    <div className="app">
      <header className="app-header">
        <h1>Lead Management</h1>
        <p className="app-subtitle">
          Sistema simples para gerenciar leads (Invited / Accepted)
        </p>
      </header>

      {/* abas */}
      <div className="tabs-container">
        <button
          className={abaAtiva === "invited" ? "tab-header active" : "tab-header"}
          onClick={() => setAbaAtiva("invited")}
        >
          Invited
        </button>

        <button
          className={
            abaAtiva === "accepted" ? "tab-header active" : "tab-header"
          }
          onClick={() => setAbaAtiva("accepted")}
        >
          Accepted
        </button>
      </div>

      <main className="tab-content">
        {abaAtiva === "invited" && (
          <LeadList
            leads={leadsInvited}
            onAccept={handleAccept}
            onDecline={handleDecline}
            showContactDetails={false}
          />
        )}

        {abaAtiva === "accepted" && (
          <LeadList
            leads={leadsAccepted}
            onAccept={null}
            onDecline={null}
            showContactDetails={true}
          />
        )}
      </main>
    </div>
  );
}

export default App;

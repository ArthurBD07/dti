import { useState } from "react";
import "./App.css";
import { initialLeads } from "./mockLeads";
import LeadList from "./components/LeadList";

function App() {
  const [abaAtiva, setAbaAtiva] = useState("invited"); // invited | accepted
  const [leads, setLeads] = useState(initialLeads);

  function handleAccept(id) {
    setLeads((prev) =>
      prev.map((lead) =>
        lead.id === id ? { ...lead, status: "accepted" } : lead
      )
    );
  }

  function handleDecline(id) {
    setLeads((prev) =>
      prev.map((lead) =>
        lead.id === id ? { ...lead, status: "declined" } : lead
      )
    );
  }

  const invitedLeads = leads.filter((lead) => lead.status === "invited");
  const acceptedLeads = leads.filter((lead) => lead.status === "accepted");

  return (
    <div className="app">
      <header className="app-header">
        <h1>Lead Management</h1>
        <p className="app-subtitle">
          Sistema simples para gerenciar leads (Invited / Accepted)
        </p>
      </header>

      {/* abas no estilo da imagem */}
      <div className="tabs-container">
        <button
          className={
            abaAtiva === "invited" ? "tab-header active" : "tab-header"
          }
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
            leads={invitedLeads}
            onAccept={handleAccept}
            onDecline={handleDecline}
          />
        )}

        {abaAtiva === "accepted" && (
          <LeadList
            leads={acceptedLeads}
            onAccept={null}
            onDecline={null}
          />
        )}
      </main>
    </div>
  );
}

export default App;

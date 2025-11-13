// src/components/LeadCard.jsx
export default function LeadCard({ lead, onAccept, onDecline }) {
  return (
    <div className="lead-card">
      {/* linha do nome e data */}
      <div className="lead-header-row">
        <div className="lead-avatar">{lead.firstName[0]}</div>

        <div className="lead-main-info">
          <div className="lead-name">{lead.firstName}</div>
          <div className="lead-date">{lead.createdAt}</div>
        </div>
      </div>

      {/* linha com suburb / categoria / job id */}
      <div className="lead-meta-row">
        <span>📍 {lead.suburb}</span>
        <span>🧰 {lead.category}</span>
        <span>Job ID: {lead.id}</span>
      </div>

      {/* descrição */}
      <p className="lead-description">{lead.description}</p>

      {/* botões e preço */}
      <div className="lead-actions-row">
        <div className="lead-buttons">
          <button
            className="btn btn-accept"
            onClick={() => onAccept && onAccept(lead.id)}
          >
            Accept
          </button>
          <button
            className="btn btn-decline"
            onClick={() => onDecline && onDecline(lead.id)}
          >
            Decline
          </button>
        </div>

        <div className="lead-price">
          <strong>${lead.price.toFixed(2)}</strong> Lead Invitation
        </div>
      </div>
    </div>
  );
}

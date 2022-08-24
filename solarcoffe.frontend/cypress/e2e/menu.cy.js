context("SideMenu", () => {
  beforeEach(() => {
    cy.visit("https://solar-coffee.vercel.app/");
  });

  it("is visits the inventory by click logo", () => {
    cy.get("#imgLogo").click();
    cy.get("#inventoryTitle").contains("Inventory");
  });

  it("is visits the Inventory by click button", () => {
    cy.get("#menuInventory").click();
    cy.get("#inventoryTitle").contains("Inventory");
  });

  it("is visits the Customers by click button", () => {
    cy.get("#menuCustomers").click();
    cy.get("#customers-title").contains("Customers");
  });

  it("is visits the Invoice by click button", () => {
    cy.get("#menuInvoice").click();
    cy.get("#invoice-title").contains("Invoice");
  });

  it("is visits the Orders by click button", () => {
    cy.get("#menuOrders").click();
    cy.get("#ordersTitle").contains("Sales orders");
  });
});

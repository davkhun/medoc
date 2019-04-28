function populateContractModel(contractId, contractName, contractSum, contractFrom, contractTo, counterpartyId, comment) {
    const model = {};
    model.ContractId = contractId;
    model.ContractName = contractName;
    model.ContractSum = contractSum;
    model.ContractFrom = contractFrom;
    model.ContractTo = contractTo;
    model.CounterpartyId = counterpartyId;
    model.Comment = comment;
    return model;
}


function collectContractModel() {
    const contractId = $('#newContractModal').attr('data-id') ? $('#newContractModal').attr('data-id'): null;
    const contractName = $('#contractNameInput').val();
    const contractSum = $('#contractSumInput').val();
    const contractFrom = $('#contractFrom').find('input').val();
    const contractTo = $('#contractTo').find('input').val();
    const counterpartyId = $('#contractCounterpartyInput').attr('data-id');
    const comment = $('#contractComment').val();
    return populateContractModel(contractId, contractName, contractSum, contractFrom, contractTo, counterpartyId, comment);
}

function clearContract() {
    $('#newContractModal').removeAttr('data-id');
    $('#contractNameInput').val('');
    $('#contractSumInput').val('0');
    $('#contractFrom').find('input').val('');
    $('#contractTo').find('input').val('');
    $('#contractCounterpartyInput').removeAttr('data-id');
    $('#contractCounterpartyInput').val('');
    $('#contractComment').val('');
}
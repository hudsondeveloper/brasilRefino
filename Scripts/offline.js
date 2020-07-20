
function geEquipamento() {
    var model = {
        Id: "",
        reg: "",
        tag: "",
        descricao_equipamento: "",
        situacao: "",
        status_Doc: "",
        descricao: "",
    };
    return model;
}


 function  carregarEquipamentos() {
    var equipamentos = JSON.parse(localStorage.getItem("equipamentos"));
    equipamentos = [];

    var url = '/ControleEstoque/CargaEquipamentos';
    fetch(url, {
        method: "POST",
    }).then(function (response) {
        return response.json();
    }).then(function (data) {
        $(data).each(function (i) {
            var model = geEquipamento();
            model.Id = data[i].Id
            model.reg = data[i].reg
            model.tag = data[i].tag
            model.descricao_equipamento = data[i].descricao_equipamento
            model.situacao = data[i].situacao
            model.status_Doc = data[i].status_Doc
            model.descricao = data[i].descricao       
            equipamentos.push(model);
        });
        localStorage.setItem("equipamentos", JSON.stringify(equipamentos));
    })
    return true;
}
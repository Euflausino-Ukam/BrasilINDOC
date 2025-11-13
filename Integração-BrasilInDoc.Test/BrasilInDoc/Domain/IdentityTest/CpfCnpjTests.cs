using Integração_BrasilInDoc.BrasilInDoc.Domain.ValueObjects;

namespace Integração_BrasilInDoc.Test.BrasilInDoc.Domain.IdentityTest
{
    [TestClass]
    public class CpfCnpjTests
    {
        // ===== CPF válidos =====
        [DataTestMethod]
        [DataRow("111.444.777-35")]
        [DataRow("11144477735")]
        [DataRow("529.982.247-25")]
        [DataRow("52998224725")]
        public void ValidCpf_ShouldReturnTrue(string cpf)
        {
            var result = IdentityValidate.ValidateCpf(cpf);
            Assert.IsTrue(result, $"Esperava CPF válido: {cpf}");
        }

        // ===== CPF inválidos =====
        [DataTestMethod]
        [DataRow("000.000.000-00")]
        [DataRow("11111111111")]
        [DataRow("123.456.789-00")]
        [DataRow("123")]
        [DataRow("")]
        [DataRow(null)]
        public void InvalidCpf_ShouldReturnFalse(string cpf)
        {
            var result = IdentityValidate.ValidateCpf(cpf);
            Assert.IsFalse(result, $"Esperava CPF inválido: {cpf ?? "null"}");
        }

        // ===== CNPJ válidos =====
        [DataTestMethod]
        [DataRow("04.252.011/0001-10")] // exemplo público
        [DataRow("04252011000110")]
        [DataRow("40.688.134/0001-61")]
        [DataRow("40688134000161")]
        public void ValidCnpj_ShouldReturnTrue(string cnpj)
        {
            var result = IdentityValidate.ValidateCnpj(cnpj);
            Assert.IsTrue(result, $"Esperava CNPJ válido: {cnpj}");
        }

        // ===== CNPJ inválidos =====
        [DataTestMethod]
        [DataRow("00.000.000/0000-00")]
        [DataRow("00000000000000")]
        [DataRow("123")]
        [DataRow("")]
        [DataRow(null)]
        public void InvalidCnpj_ShouldReturnFalse(string cnpj)
        {
            var result = IdentityValidate.ValidateCnpj(cnpj);
            Assert.IsFalse(result, $"Esperava CNPJ inválido: {cnpj ?? "null"}");
        }
    }
}
